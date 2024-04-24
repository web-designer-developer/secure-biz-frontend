namespace SecurityServices.RnR
{
    public class ListProductResponse : ResponseBase
    {
        public List<ProductInformation> Products {  get; set; }
        public ListProductResponse()
        {
            Products = new List<ProductInformation>();
        }
    }

    public class ProductInformation
    {
        public string ProductName {  get; set; }
        public string ProductId {  get; set; }
        public string ProductCode {  get; set; }
        public string ProductPrice {  get; set; }
        public ProductInformation() 
        { 
            ProductName = string.Empty; 
            ProductId = string.Empty;
            ProductCode = string.Empty;
            ProductPrice = string.Empty;
        }
    }
}
