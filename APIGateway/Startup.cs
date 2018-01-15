using APIGateway.Models;
using APIGateway.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pivotal.Discovery.Client;
using Steeltoe.CircuitBreaker.Hystrix;

namespace APIGateway
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory factory) {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        //public IConfiguration Configuration { get; }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddLogging();
            services.AddOptions();
            services.AddSingleton<IProductService, ProductService>();
            services.AddMvc();

            // Add service for ConfigServer
            services.Configure<ConfigServerData>(Configuration);
            // Add services for Service Discovery 
            services.AddDiscoveryClient(Configuration);
            // Add services for Hystrix
            services.AddHystrixCommand<GetProductCommand>("ProductService", Configuration);
            services.AddHystrixCommand<GetProductsCommand>("ProductsService", Configuration);

            services.AddHystrixMetricsStream(Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime,ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
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

            app.UseDiscoveryClient();
            app.UseHystrixRequestContext();
            app.UseHystrixMetricsStream();

        }

        private void OnStopped() {
            //RabbitMQListener.Stop();
        }

        private void OnStarted() {
            //RabbitMQListener.Start();
        }
    }
}
