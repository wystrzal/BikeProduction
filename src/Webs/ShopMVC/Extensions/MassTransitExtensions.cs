using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace ShopMVC.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal", h =>
                   {
                       h.Username("guest");
                       h.Password("guest");
                   });

                    cfg.ExchangeType = ExchangeType.Fanout;
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
