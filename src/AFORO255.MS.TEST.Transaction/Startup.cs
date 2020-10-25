using System.Reflection;
using Cross.EventBus;
using Cross.EventBus.Bus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Transactions.Data;
using Transactions.Transactions.Events;

namespace Transactions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddTransient<ITransactionsDbContext, TransactionsDbContext>();
            
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddRabbitMq();
            services.AddTransient<TransactionCreatedEvent.TransactionCreatedEventHandler>();
            services.AddTransient<IEventHandler<TransactionCreatedEvent>, TransactionCreatedEvent.TransactionCreatedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            ConfigureEventBus(app);
        }
        
        private static void ConfigureEventBus(IApplicationBuilder app)
        {
            var bus = app.ApplicationServices.GetRequiredService<IEventBus>();
            bus.Subscribe<TransactionCreatedEvent, TransactionCreatedEvent.TransactionCreatedEventHandler>();
        }
    }
}
