using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.WebApi.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.WebApi.Core.Tests
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
                foreach (HostElement hostElement in WebApiSection.Setting.HostElement)
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
