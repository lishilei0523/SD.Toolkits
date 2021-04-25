using System.Configuration;
using System.ServiceModel.NetCore.Configurations;

namespace System.ServiceModel.NetCore.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceModelSection config = (ServiceModelSection)ConfigurationManager.GetSection("system.serviceModel");

            Console.WriteLine("终结点配置");
            Console.WriteLine("------------------------------");
            foreach (EndpointElement endpoint in config.Endpoints)
            {
                Console.WriteLine(endpoint.Name);
                Console.WriteLine(endpoint.Contract);
                Console.WriteLine(endpoint.Address);
                Console.WriteLine(endpoint.Binding);
                Console.WriteLine(endpoint.BehaviorConfiguration);
                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("终结点行为配置");
            Console.WriteLine("------------------------------");
            foreach (BehaviorConfigurationElement behaviorConfiguration in config.BehaviorConfigurations)
            {
                Console.WriteLine(behaviorConfiguration.Name);
                Console.WriteLine("------------------------------");
                foreach (EndpointBehaviorElement endpointBehavior in behaviorConfiguration.EndpointBehaviors)
                {
                    Console.WriteLine(endpointBehavior.Type);
                    Console.WriteLine(endpointBehavior.Assembly);
                }
            }


            Console.ReadKey();
        }
    }
}
