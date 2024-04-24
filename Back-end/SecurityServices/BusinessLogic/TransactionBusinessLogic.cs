using SecurityServices.IBusinessLogic;
using SecurityServices.Models;
using SecurityServices.RnR;

namespace SecurityServices.BusinessLogic
{
    public class TransactionBusinessLogic : ITransactionBusinessLogic
    {
        private readonly ILogger<TransactionBusinessLogic> _logger;
        private serviceContext _context;
        public TransactionBusinessLogic(ILogger<TransactionBusinessLogic> logger, serviceContext context)
        {
            _logger = logger;
            _context = context;
        }
        public SaveTransactionResponse SaveTransaction(SaveTransactionRequest request)
        {
            var response = new SaveTransactionResponse();

            try
            {
                if(!ValidateSaveTransaction(request)) 
                {
                    throw new Exception("Invalid Save Request");
                }
                var userId = _context.Users.FirstOrDefault(x=>x.Username == request.UserName).Id;

                if(string.IsNullOrWhiteSpace(userId))
                {
                    throw new Exception("Invalid User");
                }

                var transaction = new Transaction()
                {
                    Id = Guid.NewGuid().ToString(),
                    Transactionid = request.TransactionId,
                    Transactionstatus = request.TransactionStatus,
                    Datetime  = request.TransactionDateTime,
                    Amount = request.Amount,
                    Schedule = request.Schedule,
                    Scope = request.Scope,
                    Authdetails = request.AuthDetails,
                    Clientname = request.ClientName,
                    Companyemail = request.CompanyEmail,
                    Limitation = request.Limitation,
                    Otherservices = request.OtherServices,
                    Userid = userId
                };

                _context.Transactions.Add(transaction);

                foreach(var service in request.Services)
                {
                    var product = _context.Products.FirstOrDefault(x => x.Name == service.ToLower() && x.Isavailable == 1);
                    if(product != null)
                    {
                        var pth = new Producttransactionhistory()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Productname = product.Name,
                            Productid = product.Id,
                            Transactionid = transaction.Id
                        };
                        _context.Producttransactionhistories.Add(pth);
                    }
                   
                }

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

        private bool ValidateSaveTransaction(SaveTransactionRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.TransactionId) || string.IsNullOrWhiteSpace(request.TransactionStatus) || string.IsNullOrWhiteSpace(request.CompanyEmail) || request.Amount <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
