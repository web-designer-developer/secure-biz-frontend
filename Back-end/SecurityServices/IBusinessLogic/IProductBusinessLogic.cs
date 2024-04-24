using SecurityServices.RnR;

namespace SecurityServices.IBusinessLogic
{
    public interface IProductBusinessLogic
    {
        public ListProductResponse GetProducts(ListProductRequest request);
    }
}
