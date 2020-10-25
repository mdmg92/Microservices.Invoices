using Cross.EventBus.Bus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cross.EventBus
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
        
        private static readonly string RabbitMQSectionName = "Rabbitmq";

        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMQSectionName));

            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

                return new RabbitMqBus(sp.GetService<IMediator>(), scopeFactory, configuration);
            });

            return services;
        }
    }
}
