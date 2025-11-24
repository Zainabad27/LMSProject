using Microsoft.AspNetCore.Diagnostics;

namespace LmsApp2.Api.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
            Console.WriteLine(httpContext);

            await httpContext.Response.WriteAsJsonAsync(exception.Message+" <= This is the Eror Message",cancellationToken);

            return true;
        }
    }
}
