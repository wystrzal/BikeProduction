using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logBuilder =>
            {
                logBuilder.ClearProviders();
                logBuilder.AddConsole();
                logBuilder.AddDebug();
                logBuilder.AddTraceSource("Information, ActivityTracing");
                logBuilder.AddFile("Logs/Log-Basket.txt");
            })
            .UseStartup<Startup>()
            .Build();
    }
}
