using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapStoneProject.Filters
{
    public class GlobalActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<GlobalActionFilter> _logger;

        public GlobalActionFilter(ILogger<GlobalActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestTime = DateTime.UtcNow;
            var endpoint = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;
            
            _logger.LogInformation("Request IN  - {Method} {Endpoint} at {Time}", 
                method, endpoint, requestTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            var executedContext = await next();
            
            var responseTime = DateTime.UtcNow;
            var duration = (responseTime - requestTime).TotalMilliseconds;
            
            _logger.LogInformation("Request OUT - {Method} {Endpoint} at {Time} (Duration: {Duration}ms)", 
                method, endpoint, responseTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), duration);

            if (executedContext.Result is ObjectResult result)
            {
                var response = new
                {
                    success = result.StatusCode >= 200 && result.StatusCode < 300,
                    data = result.Value,
                    message = result.StatusCode >= 200 && result.StatusCode < 300 ? "Request successful" : "Request failed",
                    trace_id = context.HttpContext.TraceIdentifier,
                    status_code = result.StatusCode,
                    request_time = requestTime,
                    response_time = responseTime,
                    duration_ms = Math.Round(duration, 2)
                };
                executedContext.Result = new ObjectResult(response)
                {
                    StatusCode = result.StatusCode
                };
            }
        }
    }
}