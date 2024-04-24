namespace SecurityServices.RnR
{
    public class ResetPasswordRequest : RequestBase
    {
        public string Password { get; set; }

        public ResetPasswordRequest() 
        {
            Password = string.Empty;
        }
    }
}
