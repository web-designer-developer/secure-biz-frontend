using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityServices.Attributes;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private serviceContext _context;
        private IProductBusinessLogic _prtbl;
        public ProductController(ILogger<UserController> logger, serviceContext context, IProductBusinessLogic prtbl)
        {
            _logger = logger;
            _context = context;
            _prtbl = prtbl;
        }

        [AuthorizationRequired]
        [HttpPost]
        [Route("list")]
        public ListProductResponse GetProductList(ListProductRequest request)
        {
            return _prtbl.GetProducts(request);
        }


    }
}
