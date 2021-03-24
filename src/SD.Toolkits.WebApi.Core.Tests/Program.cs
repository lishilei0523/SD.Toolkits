using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.WebApi.Core.Configurations;
using System.Net;

namespace SD.Toolkits.WebApi.Core.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();

            //WebHostÅäÖÃ
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    foreach (HostElement hostElement in WebApiSection.Setting.HostElement)
                    {
                        if (hostElement.IPAddress == "localhost")
                        {
                            options.ListenLocalhost(hostElement.Port);
                        }
                        else
                        {
                            IPAddress ipAddress = IPAddress.Parse(hostElement.IPAddress);
                            options.Listen(ipAddress, hostElement.Port);
                        }
                    }
                });
                webBuilder.UseStartup<Startup>();
            });

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
