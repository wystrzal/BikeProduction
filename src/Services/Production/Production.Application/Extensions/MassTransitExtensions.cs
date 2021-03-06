﻿using Common.Application.Messaging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Production.Application.Messaging.Consumers;
using RabbitMQ.Client;

namespace Production.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<OrderCreatedConsumer>();
                options.AddConsumer<OrderCanceledConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal", h =>
                   {
                       h.Username("guest");
                       h.Password("guest");
                   });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("order_created", ep =>
                    {
                        ep.Bind<OrderCreatedEvent>();
                        ep.ConfigureConsumer<OrderCreatedConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("order_canceled", ep =>
                    {
                        ep.Bind<OrderCanceledEvent>();
                        ep.ConfigureConsumer<OrderCanceledConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
