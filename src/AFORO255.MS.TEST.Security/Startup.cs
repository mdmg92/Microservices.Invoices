using System.Reflection;
using AFORO255.MS.TEST.Cross.Consul.Consul;
using AFORO255.MS.TEST.Cross.Consul.Fabio;
using AFORO255.MS.TEST.Cross.Consul.Mvc;
using Consul;
using Cross.Jwt;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Security.Data;

namespace Security
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

            services.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            services.Configure<JwtOptions>(Configuration.GetSection("BearerToken"));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            
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
                DbContext context = serciveScope.ServiceProvider.GetRequiredService<SecurityDbContext>();
                context.Database.Migrate();
            }
        }
    }
}