﻿using FastFood.Domain.Enums;
using FastFood.Domain.Extensions;

namespace FastFood.Domain.Dto
{
    public class EmptyResponseDto
    {
        public bool Succeed { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageId { get; set; }
        public int ErrorMessageCode { get; set; }

        public EmptyResponseDto()
        {
        }

        public EmptyResponseDto(bool succeed)
        {
            Succeed = succeed;
        }

        public EmptyResponseDto(string errorMessage, string errorMessageId)
        {
            ErrorMessage = errorMessage;
            ErrorMessageId = errorMessageId;
        }

        public EmptyResponseDto(AppMessageType msgType)
        {
            ErrorMessage = msgType.GetErrorMsg();
            ErrorMessageId = msgType.GetErrorCode();
        }
    }
}
