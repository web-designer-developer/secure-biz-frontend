using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.BusinessLogic
{
    public class ProductBusinessLogic : IProductBusinessLogic
    {
        private readonly ILogger<ProductBusinessLogic> _logger;
        private serviceContext _context;
        public ProductBusinessLogic(ILogger<ProductBusinessLogic> logger, serviceContext context)
        {
            _logger = logger;
            _context = context;
        }
        public ListProductResponse GetProducts(ListProductRequest request)
        {
            var response = new ListProductResponse();
            try
            {
                var products = (from p in _context.Products
                               where p.Isavailable == 1
                               select
                               new ProductInformation
                               {
                                   ProductId = p.Id,
                                   ProductCode = p.Code.ToString(),
                                   ProductName = p.Name,
                                   ProductPrice = p.Price.ToString()
                               }).ToList();

                response.Products = products;
                response.Success = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }
    }
}
