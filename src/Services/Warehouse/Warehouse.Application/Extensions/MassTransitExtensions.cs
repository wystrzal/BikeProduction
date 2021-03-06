﻿using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Warehouse.Application.Messaging.Consumers;

namespace Warehouse.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {

                options.AddConsumer<ProductionConfirmedConsumer>();
                options.AddConsumer<ProductAddedConsumer>();
                options.AddConsumer<ProductDeletedConsumer>();
                options.AddConsumer<ProductUpdatedConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("production_confirmed", ep =>
                    {
                        ep.Bind<ProductionConfirmedEvent>();
                        ep.ConfigureConsumer<ProductionConfirmedConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("product_added", ep =>
                    {
                        ep.Bind<ProductAddedEvent>();
                        ep.ConfigureConsumer<ProductAddedConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("product_updated", ep =>
                    {
                        ep.Bind<ProductUpdatedEvent>();
                        ep.ConfigureConsumer<ProductUpdatedConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("product_deleted", ep =>
                    {
                        ep.Bind<ProductDeletedEvent>();
                        ep.ConfigureConsumer<ProductDeletedConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
