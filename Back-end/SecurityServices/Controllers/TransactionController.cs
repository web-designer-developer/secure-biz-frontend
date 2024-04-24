using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityServices.Attributes;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private serviceContext _context;
        private ITransactionBusinessLogic _trbl;
        public TransactionController(ILogger<TransactionController> logger, serviceContext context, ITransactionBusinessLogic trbl)
        {
            _logger = logger;
            _context = context;
            _trbl = trbl;
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("save")]
        public SaveTransactionResponse SaveTransaction(SaveTransactionRequest request)
        {
            return _trbl.SaveTransaction(request);
        }

    }
}
