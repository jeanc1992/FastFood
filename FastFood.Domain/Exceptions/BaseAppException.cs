using FastFood.Domain.Enums;
using System;

namespace FastFood.Domain.Exceptions
{
    public abstract class BaseAppException : Exception
    {
        public AppMessageType ErrorMessageId { get; }

        protected BaseAppException(string message, AppMessageType errorMessageId)
            : base(message)
        {
            ErrorMessageId = errorMessageId;
        }

        private BaseAppException()
            : base()
        {
            ErrorMessageId = AppMessageType.ApiInvalidRequest;
        }

        private BaseAppException(string message)
            : base(message)
        {
            ErrorMessageId = AppMessageType.ApiInvalidRequest;
        }

        private BaseAppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
