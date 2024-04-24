using SecurityServices.RnR;

namespace SecurityServices.IBusinessLogic
{
    public interface ITransactionBusinessLogic
    {
        public SaveTransactionResponse SaveTransaction(SaveTransactionRequest request); 
    }
}
