using FastFood.Domain.Enums;
using System.Text.RegularExpressions;

namespace FastFood.Domain.Extensions
{
    public static class AppMessageTypeExtensions
    {
        public static string GetErrorMsg(this AppMessageType msg)
        {
            return msg switch
            {
                AppMessageType.ApiInvalidRequest => "Invalid api request",
                AppMessageType.ApiUnknownErrorOccurred => "Unknown error occurred in the api",
                AppMessageType.ApiNotFound => "The resource you were looking for was not found in the api",
                _ => throw new ArgumentOutOfRangeException(nameof(msg), msg, null)
            };
        }

        public static string GetErrorCode(this AppMessageType msg)
        {
            string[] split = Regex.Split($"{msg}", "(?<!^)(?=[A-Z])");
            int msgId = (int)msg;
            return $"{split[0].ToUpper()}_{msgId}";
        }
    }
}