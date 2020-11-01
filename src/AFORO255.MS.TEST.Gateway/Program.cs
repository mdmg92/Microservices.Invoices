using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace AFORO.MS.TEST.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((host, config) =>
                    {
                        config.AddJsonFile($"ocelot.{host.HostingEnvironment.EnvironmentName}.json", false);
                    });
                    
                    webBuilder.ConfigureAppConfiguration((host, config) =>
                    {
                        config.AddConfigServer(host.HostingEnvironment.EnvironmentName);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}