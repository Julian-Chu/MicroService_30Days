using APIGateway.Models;
using APIGateway.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pivotal.Discovery.Client;
using Steeltoe.CircuitBreaker.Hystrix;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace APIGateway
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables()
              .AddConfigServer();

            Configuration = builder.Build();
        }

        //public IConfiguration Configuration { get; }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddOptions();
            services.AddSingleton<IProductService, ProductService>();
            services.AddMvc();
            
            // Add services for Service Discovery 
            services.AddDiscoveryClient(Configuration);
            // Add services for Hystrix
            services.AddHystrixCommand<ProductServiceCommand>("ProductService", Configuration);
            // Add service for ConfigServer
            services.Configure<ConfigServerData>(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopped.Register(OnStopped);

        }

        private void OnStopped() {
            RabbitMQListener.Stop();
        }

        private void OnStarted() {
            RabbitMQListener.Start();
        }
    }
}
