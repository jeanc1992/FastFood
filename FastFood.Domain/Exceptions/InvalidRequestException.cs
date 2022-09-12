
using FastFood.Domain.Enums;

namespace FastFood.Domain.Exceptions
{
    public class InvalidRequestException : BaseAppException
    {
        public InvalidRequestException(string message, AppMessageType errorMessageId = AppMessageType.ApiInvalidRequest)
            : base(message, errorMessageId)
        {
        }
    }
}
