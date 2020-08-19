using Common.Application.Commands;
using Common.Application.Messaging;
using Delivery.Application.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Delivery.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<ProductionFinishedConsumer>();
                options.AddConsumer<PackReadyToSendConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("production_finished", ep =>
                    {
                        ep.Bind<ProductionFinishedEvent>();
                        ep.ConfigureConsumer<ProductionFinishedConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("pack_ready", ep =>
                    {
                        ep.Bind<PackReadyToSendEvent>();
                        ep.ConfigureConsumer<PackReadyToSendConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
