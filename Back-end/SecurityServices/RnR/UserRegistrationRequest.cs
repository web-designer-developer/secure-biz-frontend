namespace SecurityServices.RnR
{
    public class UserRegistrationRequest : RequestBase
    {
        public string UserName { get; set; }    

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

    }
}
