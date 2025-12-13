using Microsoft.AspNetCore.Diagnostics;

namespace LmsApp2.Api.Exceptions
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> Logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Logger.LogError(exception,"Exception Occured");
            if(exception is CustomException ex)
            {
                httpContext.Response.StatusCode = ex.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(ex.Message + " <= Error Message", cancellationToken);
                return true;
            }

            await httpContext.Response.WriteAsJsonAsync(exception?.Message+" <= Error Message",cancellationToken);

            return true;
        }
    }
}
