using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFORO255.MS.TEST.Cross.Consul.Fabio
{
    public static class Extensions
    {
        public static IServiceCollection AddFabio(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<FabioOptions>(configuration.GetSection("Fabio"));
            }

            services.AddTransient<FabioMessageHandler>();
            services.AddHttpClient<IFabioHttpClient, FabioHttpClient>()
                .AddHttpMessageHandler<FabioMessageHandler>();

            return services;
        }
    }
}