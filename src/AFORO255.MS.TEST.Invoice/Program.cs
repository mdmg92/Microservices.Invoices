using System;
using System.Threading.Tasks;
using Invoices.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Invoices
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<InvoiceDbContext>();
                    await new InvoiceDbContextSeed().SeedAsync(context);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            host.Run();
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