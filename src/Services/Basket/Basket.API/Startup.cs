using Basket.Application.Commands;
using Basket.Application.Extensions;
using CustomerOrder.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;

namespace Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                opt.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCustomAuth(Configuration);

            services.AddMediatR(typeof(AddProductCommand).Assembly);

            services.AddCustomMassTransit();

            services.AddApplicationServices();

            services.AddSwaggerDocumentation();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379";
            });

            var options = new ConfigurationOptions()
            {
                KeepAlive = 0,
                AllowAdmin = true,
                EndPoints = { { "redis", 6379 } },
                ConnectTimeout = 5000,
                ConnectRetry = 5,
                SyncTimeout = 5000,
                AbortOnConnectFail = false
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var result = JsonConvert.SerializeObject(error.Error.Message);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(result);
                        logger.LogError($"Action path: {context.Request.Path} - Message: {error.Error.Message}");
                    }
                });
            });

            app.UseSwaggerDocumention();

            app.UseRouting();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
