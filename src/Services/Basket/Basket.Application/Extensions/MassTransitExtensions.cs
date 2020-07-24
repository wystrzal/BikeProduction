using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ.Client;
using Basket.Application.Messaging.Consumers;
using Common.Application.Messaging;

namespace CustomerOrder.Application.Extensions
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
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("order_created_basket", ep =>
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
