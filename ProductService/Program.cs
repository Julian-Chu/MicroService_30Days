using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();            
        }

        /// <summary>
        /// With UseUrls("http://localhost:1234"), service discovery can't find the servie, please use UseUrls("http://*:1234")
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:1234")
                //.UseUrls("http://localhost:1234")  
                .Build();
    }
}
