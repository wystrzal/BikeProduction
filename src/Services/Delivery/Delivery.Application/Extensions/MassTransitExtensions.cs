using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using RabbitMQ.Client;
using Common.Application.Messaging;
using Delivery.Application.Messaging.Consumers;

namespace Delivery.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static void AddCustomMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<ProductionFinishedConsumer>();

                options.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("localhost", "/", h =>
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
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
