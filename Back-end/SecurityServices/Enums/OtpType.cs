
using SecurityServices.Attributes;

namespace SecurityServices.Enums
{
    public enum OtpType
    {
        [Guid("2a67ffc8-b1a0-4063-8958-1dbe20ebf509")]
        UserRegistration = 1,
        [Guid("5b070f9d-924c-4e5e-a47a-9879ff2b9794")]
        UserLogin = 2,
        [Guid("b09eb513-5fb6-4c29-80e3-b443b495b630")]
        ForgotPassword = 3
    }
}
