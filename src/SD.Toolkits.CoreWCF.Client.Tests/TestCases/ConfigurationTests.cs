using SD.Toolkits.CoreWCF.Client.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Client.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Client.Configurations.Clients;
using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Client.Tests.TestCases
{
    /// <summary>
    /// 配置文件测试
    /// </summary>
    public class ConfigurationTests
    {
        /// <summary>
        /// WCF配置节点组
        /// </summary>
        private readonly ServiceModelSectionGroup _sectionGroup = ServiceModelSectionGroup.Setting;

        /// <summary>
        /// 测试配置节点
        /// </summary>
        public void TestSections()
        {
            foreach (ConfigurationSection configurationSection in this._sectionGroup.Sections)
            {
                Console.WriteLine(configurationSection);
            }
            Console.WriteLine("------------------------------");
        }

        /// <summary>
        /// 测试终结点
        /// </summary>
        public void TestEndpoints()
        {
            foreach (ChannelEndpointElement endpoint in this._sectionGroup.Clients.Endpoints)
            {
                Console.WriteLine(endpoint.Address);
                Console.WriteLine(endpoint.Binding);
                Console.WriteLine(endpoint.BindingConfiguration);
                Console.WriteLine(endpoint.Contract);
                Console.WriteLine(endpoint.Name);
                Console.WriteLine(endpoint.BehaviorConfiguration);
                Console.WriteLine("------------------------------");
            }
        }

        /// <summary>
        /// 测试绑定
        /// </summary>
        public void TestBindings()
        {
            Console.WriteLine("BasicHttpBinding配置");
            foreach (BasicHttpBindingElement basicHttpBindingElement in this._sectionGroup.Bindings.BasicHttpBinding.Bindings)
            {
                Console.WriteLine("---------------绑定节点配置---------------");

                Console.WriteLine(basicHttpBindingElement.Name);
                Console.WriteLine(basicHttpBindingElement.MaxBufferPoolSize);
                Console.WriteLine(basicHttpBindingElement.MaxBufferSize);
                Console.WriteLine(basicHttpBindingElement.MaxReceivedMessageSize);
                Console.WriteLine(basicHttpBindingElement.CloseTimeout);
                Console.WriteLine(basicHttpBindingElement.OpenTimeout);
                Console.WriteLine(basicHttpBindingElement.ReceiveTimeout);
                Console.WriteLine(basicHttpBindingElement.SendTimeout);
                Console.WriteLine(basicHttpBindingElement.Security.Mode);
                Console.WriteLine(basicHttpBindingElement.Security.Message.ClientCredentialType);
                Console.WriteLine(basicHttpBindingElement.Security.Transport.ClientCredentialType);

                Console.WriteLine("---------------绑定配置---------------");

                Binding binding = basicHttpBindingElement.CreateBinding();
                Console.WriteLine(binding.Name);
                Console.WriteLine(binding.CloseTimeout);
                Console.WriteLine(binding.OpenTimeout);
                Console.WriteLine(binding.ReceiveTimeout);
                Console.WriteLine(binding.SendTimeout);

                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("NetTcpBinding配置");
            foreach (NetTcpBindingElement netTcpBindingElement in this._sectionGroup.Bindings.NetTcpBinding.Bindings)
            {
                Console.WriteLine("---------------绑定节点配置---------------");

                Console.WriteLine(netTcpBindingElement.Name);
                Console.WriteLine(netTcpBindingElement.MaxBufferPoolSize);
                Console.WriteLine(netTcpBindingElement.MaxBufferSize);
                Console.WriteLine(netTcpBindingElement.MaxReceivedMessageSize);
                Console.WriteLine(netTcpBindingElement.CloseTimeout);
                Console.WriteLine(netTcpBindingElement.OpenTimeout);
                Console.WriteLine(netTcpBindingElement.ReceiveTimeout);
                Console.WriteLine(netTcpBindingElement.SendTimeout);
                Console.WriteLine(netTcpBindingElement.Security.Mode);
                Console.WriteLine(netTcpBindingElement.Security.Message.ClientCredentialType);
                Console.WriteLine(netTcpBindingElement.Security.Transport.ClientCredentialType);

                Console.WriteLine("---------------绑定配置---------------");

                Binding binding = netTcpBindingElement.CreateBinding();
                Console.WriteLine(binding.Name);
                Console.WriteLine(binding.CloseTimeout);
                Console.WriteLine(binding.OpenTimeout);
                Console.WriteLine(binding.ReceiveTimeout);
                Console.WriteLine(binding.SendTimeout);

                Console.WriteLine("------------------------------");
            }

            Console.WriteLine("BasicHttpBinding配置");
            foreach (WSHttpBindingElement wsHttpBindingElement in this._sectionGroup.Bindings.WSHttpBinding.Bindings)
            {
                Console.WriteLine("---------------绑定节点配置---------------");

                Console.WriteLine(wsHttpBindingElement.Name);
                Console.WriteLine(wsHttpBindingElement.MaxBufferPoolSize);
                Console.WriteLine(wsHttpBindingElement.MaxReceivedMessageSize);
                Console.WriteLine(wsHttpBindingElement.CloseTimeout);
                Console.WriteLine(wsHttpBindingElement.OpenTimeout);
                Console.WriteLine(wsHttpBindingElement.ReceiveTimeout);
                Console.WriteLine(wsHttpBindingElement.SendTimeout);
                Console.WriteLine(wsHttpBindingElement.Security.Mode);
                Console.WriteLine(wsHttpBindingElement.Security.Message.ClientCredentialType);
                Console.WriteLine(wsHttpBindingElement.Security.Transport.ClientCredentialType);

                Console.WriteLine("---------------绑定配置---------------");

                Binding binding = wsHttpBindingElement.CreateBinding();
                Console.WriteLine(binding.Name);
                Console.WriteLine(binding.CloseTimeout);
                Console.WriteLine(binding.OpenTimeout);
                Console.WriteLine(binding.ReceiveTimeout);
                Console.WriteLine(binding.SendTimeout);

                Console.WriteLine("------------------------------");
            }
        }

        /// <summary>
        /// 测试行为
        /// </summary>
        public void TestBehaviors()
        {
            Console.WriteLine("终结点行为配置");
            Console.WriteLine("------------------------------");
            foreach (BehaviorConfigurationElement behaviorConfigurationElement in this._sectionGroup.Behaviors.BehaviorConfigurations)
            {
                Console.WriteLine(behaviorConfigurationElement.Name);
                foreach (EndpointBehaviorElement endpointBehavior in behaviorConfigurationElement.EndpointBehaviors)
                {
                    Console.WriteLine(endpointBehavior.Assembly);
                    Console.WriteLine(endpointBehavior.Type);
                }
                Console.WriteLine("------------------------------");
            }
        }
    }
}
