using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNetCore;
using SD.Toolkits.AspNetCore.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.WebApiCore.Tests
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
                foreach (HostElement hostElement in AspNetCoreSection.Setting.HostElements)
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
