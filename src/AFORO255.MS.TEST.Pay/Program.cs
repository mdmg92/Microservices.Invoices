using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Pay
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
                        config.AddConfigServer(host.HostingEnvironment.EnvironmentName);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}