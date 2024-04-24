namespace SecurityServices.RnR
{
    public class ValidateTokenResponse : ResponseBase
    {
        public bool IsValid { get; set; }

        public ValidateTokenResponse() 
        { 
            IsValid = false;
        }

    }
}
