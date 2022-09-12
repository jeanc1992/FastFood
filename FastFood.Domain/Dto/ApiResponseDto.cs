
using FastFood.Domain.Enums;

namespace FastFood.Domain.Dto
{
    public class ApiResponseDto<T> : EmptyResponseDto
    {
        public T Result { get; set; }
         
        public ApiResponseDto()
        {
        }

        public ApiResponseDto(T result)
        {
            Succeed = true;
            Result = result;
        }

        public ApiResponseDto(string errorMsg, string errorCode)
        {
            ErrorMessage = errorMsg;
            ErrorMessageId = errorCode;
        }

        public ApiResponseDto(AppMessageType msgType) : base(msgType)
        {
        }

        public ApiResponseDto(T result, AppMessageType msgType) : base(msgType)
        {
            Result = result;
            Succeed = true;
        }

        public ApiResponseDto(bool succeed, T result, AppMessageType msgType) : base(msgType)
        {
            Result = result;
            Succeed = succeed;
        }
    }
}
