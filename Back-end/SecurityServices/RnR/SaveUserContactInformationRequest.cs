using System.ComponentModel.DataAnnotations;

namespace SecurityServices.RnR
{
    public class SaveUserContactInformationRequest : RequestBase
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Message { get; set; }

        public SaveUserContactInformationRequest()
        {
            Email = string.Empty;
            Name = string.Empty;
            Company = string.Empty;
            Message = string.Empty;
        }
    }
}
