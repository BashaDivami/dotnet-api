using System.Net;
using System.Text.Json;

namespace CapStoneProject.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                TraceId = context.TraceIdentifier
            };

            switch (exception)
            {
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.ErrorCode = "UNAUTHORIZED";
                    response.Message = exception.Message;
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.ErrorCode = "INVALID_OPERATION";
                    response.Message = exception.Message;
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.ErrorCode = "RESOURCE_NOT_FOUND";
                    response.Message = exception.Message;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.ErrorCode = "INTERNAL_SERVER_ERROR";
                    response.Message = "An internal server error occurred";
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public string ErrorCode { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string TraceId { get; set; } = null!;
    }
}
