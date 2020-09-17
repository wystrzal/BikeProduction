using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Production.Infrastructure.Data;
using System;

namespace Production.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureLogging(logBuilder =>
                 {
                     logBuilder.ClearProviders();
                     logBuilder.AddConsole();
                     logBuilder.AddDebug();
                     logBuilder.AddTraceSource("Information, ActivityTracing");
                     logBuilder.AddFile("Logs/Log-Production.txt");
                 })
                .UseStartup<Startup>()
                .Build();
    }
}
