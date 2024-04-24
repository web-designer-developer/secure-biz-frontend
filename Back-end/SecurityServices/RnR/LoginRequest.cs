namespace SecurityServices.RnR
{
    public class LoginRequest : RequestBase
    {
        public string Password { get; set; }
        public LoginRequest() 
        {
            Password = string.Empty;
        }
    }
}
