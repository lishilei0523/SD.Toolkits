using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SD.Toolkits.AspNet;

namespace SD.Toolkits.AspNetCore.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            //WebHostÅäÖÃ
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    foreach (int httpPort in AspNetSetting.HttpPorts)
                    {
                        options.ListenAnyIP(httpPort);
                    }
                });
                webBuilder.UseStartup<Startup>();
            });

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
