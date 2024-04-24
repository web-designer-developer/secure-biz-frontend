using SecurityServices.RnR;

namespace SecurityServices.IBusinessLogic
{
    public interface IContactBusinessLogic
    {
        public SaveUserContactInformationResponse SaveUserContactInformation(SaveUserContactInformationRequest request);
    }
}
