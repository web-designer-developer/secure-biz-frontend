namespace SecurityServices.RnR
{
    public class SaveTransactionRequest : RequestBase
    {
        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
        public string ClientName { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public string CompanyEmail { get; set; }
        public string Scope { get; set; }
        public string Limitation { get; set; }
        public string AuthDetails { get; set; }
        public string Schedule { get; set; }
        public string OtherServices { get; set; }
        public List<string> Services { get; set; }

        public SaveTransactionRequest() 
        { 
            TransactionId = string.Empty; 
            TransactionStatus = string.Empty;
            ClientName = string.Empty;
            TransactionDateTime = DateTime.Now;
            Amount = 0;
            CompanyEmail = string.Empty;
            Scope = string.Empty;
            Limitation = string.Empty;
            AuthDetails = string.Empty;
            Schedule = string.Empty;
            OtherServices = string.Empty;
            Services = new List<string>();

        }
    }
}
