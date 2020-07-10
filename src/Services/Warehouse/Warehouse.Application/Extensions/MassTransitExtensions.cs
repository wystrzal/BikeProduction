using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Warehouse.Application.Messaging;
using Warehouse.Application.Messaging.Consumers;

namespace Warehouse.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {

                options.AddConsumer<ConfirmProductionConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("confirm_production", ep =>
                    {
                        ep.Bind<ConfirmProductionEvent>();
                        ep.ConfigureConsumer<ConfirmProductionConsumer>(provider);
                    });

                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
