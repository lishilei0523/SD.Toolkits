using Topshelf;

namespace SD.Toolkits.WebApi.Tests
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(config =>
            {
                config.Service<ServiceLauncher>(host =>
                {
                    host.ConstructUsing(name => new ServiceLauncher());
                    host.WhenStarted(launcher => launcher.Start());
                    host.WhenStopped(launcher => launcher.Stop());
                });
                config.RunAsLocalSystem();

                config.SetServiceName("SD.Toolkits.WebApi.Tests");
                config.SetDisplayName("SD.Toolkits.WebApi.Tests");
                config.SetDescription("SD.Toolkits.WebApi.Tests");
            });
        }
    }
}
