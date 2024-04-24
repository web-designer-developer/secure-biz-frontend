namespace SecurityServices.RnR
{
    public class LoginResponse :ResponseBase
    {
        public bool IsAccountLocked { get; set; }

        public LoginResponse()
        {
            IsAccountLocked = false;
        }

    }
}
