using FluentEmail.Core;
using SecurityServices.Common;
using SecurityServices.Enums;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;
using SecurityServices.Utlis;

namespace SecurityServices.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private readonly ILogger<UserBusinessLogic> _logger;
        private serviceContext _context;
        private IFluentEmail _fluentEmail;
        public UserBusinessLogic(ILogger<UserBusinessLogic> logger, serviceContext context, IFluentEmail fluentEmail)
        {
            _logger = logger;
            _context = context;
            _fluentEmail = fluentEmail;
        }

        public async Task<UserRegistrationResponse> RegisterUserAsync(UserRegistrationRequest request)
        {
            var response = new UserRegistrationResponse();
            try
            {
                if (!ValidateRegistrationRequest(request))
                {
                    throw new Exception("Invalid Registration Request");
                }
                var existingUser = (from u in _context.Users
                                    where u.Username == request.UserName
                                    select u).FirstOrDefault();
                if (existingUser != null)
                {
                    if (existingUser.Emailverified == 1)
                        throw new Exception("Email Already Exists!!");

                    _context.Remove(existingUser);
                }

                var salt = Hash.GenerateSalt(6);
                var hash = Hash.GenerateHash(request.Password, salt);

                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = request.UserName,
                    Firstname = request.FirstName,
                    Lastname = request.LastName,
                    Companyname = request.CompanyName,
                    Emailverified = 0,
                    Salt = salt,
                    Hash = hash,
                    Failedloginattempts = 0
                };

                var otp = OtpGenerator.GenerateOtp(6);
                response.OtpSent = await EmailSender.SendEmail(_fluentEmail, user.Username, otp, (int)OtpType.UserRegistration);

                if (response.OtpSent)
                {
                    user.Lastotp = otp;
                    user.Lastotpsent = DateTime.UtcNow;
                    response.UserName = user.Username;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogCritical("Unable to send Mail");
                    throw new Exception("Please use a valid email address to register!");
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
            response.UserName = request.UserName;
            response.Success = true;
            return response;

        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = new LoginResponse();

            try
            {
                if (!ValidateLoginRequest(request))
                {
                    throw new Exception("Invalid Login Request");
                }

                var existingUser = (from u in _context.Users
                                    where u.Username == request.UserName && u.Emailverified == 1
                                    select u).FirstOrDefault();
                if (existingUser == null)
                {
                    throw new Exception("Invalid Username or Password");
                }

                if (existingUser.Failedloginattempts > 2 && existingUser.Lastfailedattempt != null && existingUser.Lastfailedattempt >= DateTime.UtcNow.AddHours(-1))
                {
                    response.IsAccountLocked = true;
                    throw new Exception("Your Account is Locked please try again after sometime.");
                }

                var isValidPassword = Hash.Compare(request.Password, existingUser.Salt, existingUser.Hash);
                if (!isValidPassword)
                {
                    throw new Exception("Invalid Username or Password");
                }

                var otp = OtpGenerator.GenerateOtp(6);
                var otpSent = await EmailSender.SendEmail(_fluentEmail, existingUser.Username, otp, (int)OtpType.UserLogin);
                if (otpSent)
                {
                    existingUser.Lastotp = otp;
                    existingUser.Lastotpsent = DateTime.UtcNow;
                    response.UserName = existingUser.Username;
                    _context.Users.Update(existingUser);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogCritical("Unable to send Mail");
                    throw new Exception("Unable to Login. Please try after sometime.");
                }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }
            response.Success = true;
            return response;
        }

        public async Task<GetResetCodeResponse> VerifyEmail(GetResetCodeRequest request)
        {
            var response = new GetResetCodeResponse();
            try
            {
                var user = (from u in _context.Users
                            where u.Username == request.UserName && u.Emailverified == 1
                            select u).FirstOrDefault();
                if (user != null)
                {

                    var otp = OtpGenerator.GenerateOtp(6);
                    var OtpSent = await EmailSender.SendEmail(_fluentEmail, user.Username, otp, (int)OtpType.UserRegistration);

                    if (OtpSent)
                    {
                        user.Lastotp = otp;
                        user.Lastotpsent = DateTime.UtcNow;
                        response.UserName = user.Username;
                        _context.Users.Update(user);
                        _context.SaveChanges();
                        response.isValidEmail = true;
                        response.Success = true;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }

        }

        public async Task<VerifyOtpResponse> VerifyOtp(VerifyOtpRequest request)
        {
            var response = new VerifyOtpResponse();

            try
            {
                var user = _context.Users.Where(x => x.Username == request.UserName).FirstOrDefault();

                if (user != null)
                {
                    if(user.Failedloginattempts > 2)
                    {
                        //reset account lock 
                        if (user.Lastfailedattempt != null && user.Lastfailedattempt <= DateTime.UtcNow.AddHours(-1))
                        {
                            user.Failedloginattempts = 0;
                        }
                        else
                        {
                            response.IsAccountLocked = true;
                            throw new Exception("Your Account is Locked please try again after sometime.");
                        }
                    }

                    if (((DateTime)user.Lastotpsent).AddMinutes(10) >= DateTime.UtcNow && user.Lastotp == request.Otp)
                    {
                        response.IsValid = true;
                        response.Success = true;
                        response.UserName = request.UserName;

                        if (request.OtpType == "1")
                        {
                            user.Emailverified = 1;
                        }

                        else if (request.OtpType == "2")
                        {
                            var sessionId = Guid.NewGuid().ToString();
                            TokenManager tokenManager = new TokenManager(_context);
                            var usertoken = new Usertoken
                            {
                                Id = Guid.NewGuid().ToString(),
                                Sessionid = sessionId,
                                Generatedat = DateTime.UtcNow,
                                //Expiresat = DateTime.UtcNow.AddDays(1),
                                Expiresat = DateTime.UtcNow.AddHours(2),
                                Userid = user.Id,
                                Token = tokenManager.GenerateToken(sessionId, user.Id)
                            };
                            _context.Usertokens.Add(usertoken);
                            response.UserSessionToken = usertoken.Token;
                        }
                        else if (request.OtpType == "3")
                        {
                            response.IsValid = true;
                        }
                        else
                        {
                            throw new Exception("Invalid RequestType!!");
                        }
                    }
                    else
                    {
                        user.Lastfailedattempt = DateTime.UtcNow;
                        user.Failedloginattempts += 1;
                        if(user.Failedloginattempts > 2)
                        {
                            response.IsAccountLocked = true;
                        }
                    }
                }
                else
                {
                    throw new Exception("Otp is no more valid!!");
                }
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var response = new ResetPasswordResponse();
            try
            {
                var user = (from u in _context.Users
                            where u.Username == request.UserName && u.Emailverified == 1
                            select u).FirstOrDefault();
                if (user != null)
                {
                    var salt = Hash.GenerateSalt(6);
                    var hash = Hash.GenerateHash(request.Password, salt);
                    user.Salt = salt;
                    user.Hash = hash;
                    _context.Update(user);
                    _context.SaveChanges();
                }
                else { throw new Exception("User Doesn't exist!!"); }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
            response.Success = true;
            return response;
        }

        public LogoutResponse LogOut(LogoutRequest request)
        {
            var response = new LogoutResponse();
            try
            {
                TokenManager tokenManager = new TokenManager(_context);
                bool success = tokenManager.ExpireToken(request.Token, request.UserName);
                response.Success = true;
                return response;
            }
            catch (Exception)
            {
                response.Success = false;
                return response;
            }
        }


        private bool ValidateLoginRequest(LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.LogCritical("Invalid User Login Request");
                return false;
            }
            return true;
        }

        private bool ValidateRegistrationRequest(UserRegistrationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.CompanyName))
            {
                _logger.LogCritical("Invalid User Registration Request");
                return false;
            }

            return true;
        }


    }
}
