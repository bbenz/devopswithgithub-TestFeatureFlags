using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// Added to handle Configuration manger
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
/// end
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestFeatureFlags
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        
        ///original code without app config manager
        ///public void ConfigureServices(IServiceCollection services)
        ///{
        ///    services.AddControllersWithViews();
        ///}

        ///replaced code with app config manager reference
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddFeatureManagement()
              .AddFeatureFilter<PercentageFilter>()
              .AddFeatureFilter<TimeWindowFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
