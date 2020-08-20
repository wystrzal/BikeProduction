using Catalog.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    await DataSeed.AddSeed(context);
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
                    logBuilder.AddFile("Logs/Log-Catalog.txt");
                })
                .UseStartup<Startup>()
                .Build();
    }
}
