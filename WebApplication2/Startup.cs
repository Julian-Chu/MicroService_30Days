﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProductService
{
    public class Startup
    {
        IServerAddressesFeature serverAddressesFeature;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            //serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>(); //Get Kestrel Uri 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopped.Register(OnStopped);

            app.Use(next =>
            {
                return async (context) =>
                {
                    await context.Response.WriteAsync("Application will shut down");
                    await Task.Delay(10000);
                    appLifetime.StopApplication();
                };
            });
        }

        private void OnStarted() {
            /// When this service ist started, it will request a registry in API gateway service.
            Console.WriteLine("Starting");
            using (HttpClient client = new HttpClient()) {
                //string data = serverAddressesFeature.Addresses.FirstOrDefault().ToString(); //Kestrel
                string IISExpressIP = "http://localhost:4147";
                string json = JsonConvert.SerializeObject(new{ Name="ProductService", Location=IISExpressIP });
                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:2343//api/ServiceRegistry", contentPost).Result;                
            }
        }

        private void OnStopped() {
            using (HttpClient client = new HttpClient()) {
                string Uri = "http://localhost:2343/api/ServiceRegistry";
                string IISExpressIP = "http://localhost:4147";
                var service = new { Name = "ProductService", Location = IISExpressIP };
                string json = JsonConvert.SerializeObject(service);
                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                string requestUri = $"{Uri}/{service.Name}";
                HttpResponseMessage response = client.DeleteAsync(requestUri).Result;
            }
            Console.WriteLine("Stopped");
        }

    }
}
