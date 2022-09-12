using FastFood.Domain.Dto;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, logger);
            }
        }

        private Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            ILogger logger)
        {
            logger.LogInformation($"{nameof(HandleExceptionAsync)}: Handling exception of type = {exception.GetType()}....");

            var code = HttpStatusCode.InternalServerError;

            var response = new EmptyResponseDto();

            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case NotFoundException notFoundEx:
                    code = HttpStatusCode.NotFound;
                    response.ErrorMessageId = notFoundEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = notFoundEx.Message;
                    response.ErrorMessageCode = (int)notFoundEx.ErrorMessageId;
                    break;
                case InvalidRequestException invEx:
                    code = HttpStatusCode.BadRequest;
                    response.ErrorMessageId = invEx.ErrorMessageId.GetErrorCode();
                    response.ErrorMessage = invEx.Message;
                    response.ErrorMessageCode = (int)invEx.ErrorMessageId;
                    break;
                default:
                    logger.LogError(exception, $"{nameof(HandleExceptionAsync)}: Unknown exception was captured");
                    break;
            }
            #if DEBUG
            response.ErrorMessage += $". Ex: {exception}";
            #endif
            context.Response.StatusCode = (int)code;

            logger.LogInformation(
                $"{nameof(HandleExceptionAsync)}: The final response is going to " +
                $"be = {response.ErrorMessageId} - {response.ErrorMessage}");
            return context.Response.WriteAsync(JsonSerializer.Serialize(
                response, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}
