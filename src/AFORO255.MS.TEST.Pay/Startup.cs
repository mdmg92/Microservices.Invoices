using System.Reflection;
using AFORO255.MS.TEST.Cross.Consul.Consul;
using AFORO255.MS.TEST.Cross.Consul.Fabio;
using AFORO255.MS.TEST.Cross.Consul.Mvc;
using Consul;
using Cross.EventBus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pay.Data;

namespace Pay
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

            services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("Mysql"));
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pay app"
                });
            });
            
            services.AddRabbitMq();
            
            services.AddScoped<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddConsul();

            services.AddFabio();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime appLife,
            IConsulClient consulClient
        ) {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "App Pay");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            var service = app.UseConsul();
            appLife.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(service);
            });
        }
        
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serciveScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DbContext context = serciveScope.ServiceProvider.GetRequiredService<PaymentDbContext>();
                context.Database.Migrate();
            }
        }
    }
}