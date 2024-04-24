namespace SecurityServices.RnR
{
    public class RequestBase
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public RequestBase()
        {
            UserName = string.Empty;
            Token = string.Empty;
        }
    }
}
