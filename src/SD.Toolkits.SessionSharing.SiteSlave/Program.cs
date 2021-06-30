using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;
using SD.Toolkits.AspNet.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.SessionSharing.SiteSlave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            //WebHostÅäÖÃ
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                ICollection<string> urls = new HashSet<string>();
                foreach (HostElement hostElement in AspNetSection.Setting.HostElements)
                {
                    urls.Add(hostElement.Url);
                }

                webBuilder.UseKestrel();
                webBuilder.UseUrls(urls.ToArray());
                webBuilder.UseStartup<Startup>();
            });

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
