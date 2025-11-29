namespace LmsApp2.Api
{
    public static class AppStartupLogs
    {
        public static void StartupLog(this WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();
            logger.LogWarning("{AppName} started ", app.Environment.ApplicationName);
        }
    }

}
