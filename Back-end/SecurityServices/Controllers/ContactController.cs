using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly ILogger<ContactController> _logger;
        private serviceContext _context;
        private IContactBusinessLogic _contactbl;

        public ContactController(ILogger<ContactController> logger, serviceContext context, IContactBusinessLogic contactbl)
        {
            _logger = logger;
            _context = context;
            _contactbl = contactbl;
        }
        [HttpPost]
        [Route("save")]
        public SaveUserContactInformationResponse SaveUserContactInformation(SaveUserContactInformationRequest request)
        {
            return _contactbl.SaveUserContactInformation(request); 
        }

    }
}
