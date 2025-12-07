using Microsoft.AspNetCore.Diagnostics;

namespace LmsApp2.Api.Exceptions
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> Logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            Console.WriteLine(httpContext);


            Logger.LogError(exception,"Exception Occured");

            await httpContext.Response.WriteAsJsonAsync(exception?.Message+" <= Error Message",cancellationToken);

            return true;
        }
    }
}
