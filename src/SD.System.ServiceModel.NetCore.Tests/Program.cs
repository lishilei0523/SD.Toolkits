using System.ServiceModel.NetCore.Configurations;

namespace System.ServiceModel.NetCore.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceModelSection configuration = ServiceModelSection.Setting;

            Console.WriteLine("终结点配置");
            Console.WriteLine("------------------------------");
            foreach (EndpointElement endpoint in configuration.EndpointElements)
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
            foreach (BehaviorConfigurationElement behaviorConfiguration in configuration.BehaviorConfigurationElements)
            {
                Console.WriteLine(behaviorConfiguration.Name);
                foreach (EndpointBehaviorElement endpointBehavior in behaviorConfiguration.EndpointBehaviorElements)
                {
                    Console.WriteLine(endpointBehavior.Type);
                    Console.WriteLine(endpointBehavior.Assembly);
                }
                Console.WriteLine("------------------------------");
            }

            Console.ReadKey();
        }
    }
}
