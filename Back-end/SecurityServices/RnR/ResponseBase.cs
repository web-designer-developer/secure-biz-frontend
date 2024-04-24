namespace SecurityServices.RnR
{
    public class ResponseBase
    {
        public List<string> Errors { get; set; }
        public List<string> ErrorCodes { get; set; }
        public bool Success { get; set; }
        public string UserName { get; set; }

        public ResponseBase() 
        {
            Errors = new List<string>();
            ErrorCodes = new List<string>();    
            Success = false;
            UserName = string.Empty;
        }

    }
}
