namespace SecurityServices.RnR
{
    public class VerifyOtpResponse : ResponseBase
    {
        public bool IsValid { get; set; }
        public bool IsAccountLocked { get; set; }
        public string UserSessionToken { get; set; }


        public VerifyOtpResponse() 
        {
            IsValid = false;
            IsAccountLocked = false;
            UserSessionToken = string.Empty;
        }
    }
}
