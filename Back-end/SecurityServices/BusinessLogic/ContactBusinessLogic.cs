using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.BusinessLogic
{
    public class ContactBusinessLogic : IContactBusinessLogic
    {
        private readonly ILogger<ContactBusinessLogic> _logger;
        private serviceContext _context;
        public ContactBusinessLogic(ILogger<ContactBusinessLogic> logger, serviceContext context)
        {
            _logger = logger;
            _context = context;
        }
        public SaveUserContactInformationResponse SaveUserContactInformation(SaveUserContactInformationRequest request)
        {
            var response = new SaveUserContactInformationResponse();

            try
            {
                var contactInfo = new Usercontact()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    Name = request.Name,
                    Company = request.Company,
                    Message = request.Message
                };
                _context.Add(contactInfo);
                _context.SaveChanges();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
            return response;
        }
    }
}
