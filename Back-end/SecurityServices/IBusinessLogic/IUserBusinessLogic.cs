using SecurityServices.RnR;

namespace SecurityServices.IBusinessLogic
{
    public interface IUserBusinessLogic
    {
        public Task<UserRegistrationResponse> RegisterUserAsync(UserRegistrationRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<VerifyOtpResponse> VerifyOtp(VerifyOtpRequest request);
        public  Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        public LogoutResponse LogOut(LogoutRequest request);
        public Task<GetResetCodeResponse> VerifyEmail(GetResetCodeRequest request);

    }
}
