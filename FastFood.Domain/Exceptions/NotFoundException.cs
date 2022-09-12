using FastFood.Domain.Enums;

namespace FastFood.Domain.Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException(string message, AppMessageType errorMessageId = AppMessageType.ApiNotFound)
            : base(message, errorMessageId)
        {
        }

        public NotFoundException(string name, long id, AppMessageType errorMessageId = AppMessageType.ApiNotFound)
            : base($"Resource = {name} associated to id = {id} was not found.", errorMessageId)
        {
        }
    }
}
