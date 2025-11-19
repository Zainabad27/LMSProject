using Microsoft.AspNetCore.Diagnostics;

namespace LmsApp2.Api.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            await httpContext.Response.WriteAsJsonAsync(exception.Message,cancellationToken);

            return true;
        }
    }
}
