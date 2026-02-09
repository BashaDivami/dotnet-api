using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CapStoneProject.Filters
{
    public class GlobalResponseFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result != null)
            {
                var response = new
                {
                    success = result.StatusCode >= 200 && result.StatusCode < 300,
                    data = result.Value,
                    message = result.StatusCode >= 200 && result.StatusCode < 300 ? "Request successful" : "Request failed",
                    trace_id = context.HttpContext.TraceIdentifier,
                    status_code = result.StatusCode
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = result.StatusCode
                };
            }
            await next();
        }
    }
}