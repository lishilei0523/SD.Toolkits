using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Toolkits.Redis;

namespace SD.Toolkits.SessionSharing.SiteMaster
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
            services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = RedisManager.RedisConfigurationOptions);
            services.AddDataProtection(options => options.ApplicationDiscriminator = "MyWebSite");
        }

        public void Configure(IApplicationBuilder appbBuilder)
        {
            appbBuilder.UseRouting();
            appbBuilder.UseSession();
            appbBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
