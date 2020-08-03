using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ.Client;
using CustomerOrder.Application.Messaging.Consumers;
using Common.Application.Messaging;

namespace CustomerOrder.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<ChangeOrderStatusConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://host.docker.internal",  h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    cfg.ReceiveEndpoint("change_order_status", ep =>
                    {
                        ep.Bind<ChangeOrderStatusEvent>();
                        ep.ConfigureConsumer<ChangeOrderStatusConsumer>(provider);
                    });

                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
