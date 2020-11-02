using AFORO255.MS.TEST.Cross.Kafka.Bus;
using AFORO255.MS.TEST.Cross.Kafka.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFORO255.MS.TEST.Cross.Kafka
{
    public static class Extensions
    {
        public static IServiceCollection AddKafka(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddSingleton<IEventBus, KafkaBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

                return new KafkaBus(scopeFactory, configuration);
            });

            return services;
        }

        public static IServiceCollection AddMessageHandler<TMessage, THandler>(this IServiceCollection services)
            where TMessage : Event, new()
            where THandler : IEventHandler<TMessage>
        {
            IConfiguration configuration;
            IEventBus kafka;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
                kafka = serviceProvider.GetService<IEventBus>();
            }
            
            services.AddHostedService<KafkaBus.KafkaMessageConsumer<TMessage>>();
            kafka.Subscribe<TMessage, THandler>();

            return services;
        }
    }
}
