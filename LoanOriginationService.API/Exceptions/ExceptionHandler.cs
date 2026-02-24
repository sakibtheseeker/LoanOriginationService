using Microsoft.AspNetCore.Diagnostics;

namespace LoanOriginationService.API.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        ILogger<ExceptionHandler> Logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.Logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            int statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                statusCode = statusCode,
                message = exception.Message
            };

            Logger.LogError(exception, exception.Message);

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
