using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityServices.Attributes;
using SecurityServices.BusinessLogic;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;
using SecurityServices.Utlis;

namespace SecurityServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private serviceContext _context;
        private IUserBusinessLogic _userbl;

        public UserController(ILogger<UserController> logger, serviceContext context, IUserBusinessLogic userbl)
        {
            _logger = logger;
            _context = context;
            _userbl = userbl;
        }


        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _userbl.Login(request);
        }

        [HttpPost]
        [Route("register")]
        public async Task<UserRegistrationResponse> RegisterUser(UserRegistrationRequest request)
        {
            return await _userbl.RegisterUserAsync(request);
        }

        [HttpPost]
        [Route("verifyotp")]
        public async Task<VerifyOtpResponse> VerifyOtp(VerifyOtpRequest request)
        {
            return await _userbl.VerifyOtp(request);
        }

        [HttpPost]
        [Route("verifyemail")]
        public async Task<GetResetCodeResponse> VerifyEmail(GetResetCodeRequest request)
        {
            return await _userbl.VerifyEmail(request);
        }


        [HttpPost]
        [Route("reset")]
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            return await _userbl.ResetPassword(request);
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("logout")]
        public LogoutResponse LogOut(LogoutRequest request)
        {
            request.Token = HttpContext.Request.Headers["Token"];
            request.UserName = HttpContext.Request.Headers["UserName"];
            return _userbl.LogOut(request);
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("validatetoken")]
        public ValidateTokenResponse ValidateToken(ValidateTokenRequest request)
        {
            var response = new ValidateTokenResponse() 
            { 
                Success = true,
                IsValid = true,
                UserName = HttpContext.Request.Headers["UserName"]
        };
            return response;
        }
    }
}
