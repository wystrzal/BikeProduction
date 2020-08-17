using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ.Client;
using Common.Application.Messaging;
using Catalog.Application.Messaging.Consumers;

namespace Catalog.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<OrderCreatedConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal",  h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("order_created_catalog", ep =>
                    {
                        ep.Bind<OrderCreatedEvent>();
                        ep.ConfigureConsumer<OrderCreatedConsumer>(provider);
                    });

                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
