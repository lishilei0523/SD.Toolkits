using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
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

            IDataProtectionBuilder dataProtectionBuilder = services.AddDataProtection(options => options.ApplicationDiscriminator = "slamdunk.com");
            dataProtectionBuilder.SetApplicationName("slamdunk.com");
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(RedisManager.Instance, "slamdunk.com");

        }

        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseRouting();
            appBuilder.UseSession();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
