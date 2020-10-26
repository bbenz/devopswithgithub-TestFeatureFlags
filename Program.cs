using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestFeatureFlags
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

///original code without app config manager
///        public static IHostBuilder CreateHostBuilder(string[] args) =>
///            Host.CreateDefaultBuilder(args)
///                .ConfigureWebHostDefaults(webBuilder =>
///                {
///                    webBuilder.UseStartup<Startup>();
///                });

///replaced code with app config manager reference
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
            webBuilder.ConfigureAppConfiguration(config =>
            {
                var settings = config.Build();
                var connection = settings.GetConnectionString("AppConfig");
                config.AddAzureAppConfiguration(options =>
                    options.Connect(connection).UseFeatureFlags());
            }).UseStartup<Startup>());

    }
}
