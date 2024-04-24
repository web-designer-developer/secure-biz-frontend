namespace SecurityServices.RnR
{
    public class VerifyOtpRequest : RequestBase
    {
        public string Otp { get; set; }
        public string OtpType { get; set; }

        public VerifyOtpRequest() 
        { 
            Otp =  string.Empty;
            OtpType = string.Empty;
        }

    }
}
