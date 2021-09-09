using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Toolkits.AspNet;
using SD.Toolkits.Redis;

namespace SD.Toolkits.SessionSharing.SiteSlave
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Ìí¼ÓSession
            services.AddSession();
            services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = RedisManager.RedisConfigurationOptions);

            //Ìí¼ÓSession¹²Ïí
            services.AddDataProtection(options =>
            {
                options.ApplicationDiscriminator = AspNetSection.Setting.ApplicationName.Value;
            });
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
