namespace SecurityServices.RnR
{
    public class UserRegistrationResponse : ResponseBase
    {
        public string UserName { get; set; }
        public bool OtpSent { get; set; }

    }
}
