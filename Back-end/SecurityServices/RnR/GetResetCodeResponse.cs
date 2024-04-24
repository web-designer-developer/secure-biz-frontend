namespace SecurityServices.RnR
{
    public class GetResetCodeResponse : ResponseBase
    {
        public bool isValidEmail { get; set; }
        public GetResetCodeResponse()
        {
            isValidEmail = false;
        }
    }
}
