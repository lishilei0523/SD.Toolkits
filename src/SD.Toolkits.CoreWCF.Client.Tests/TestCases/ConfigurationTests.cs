using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.CoreWCF.Client.Configurations.Behaviors;
using SD.Toolkits.CoreWCF.Client.Configurations.Bindings;
using SD.Toolkits.CoreWCF.Client.Configurations.Clients;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SD.Toolkits.CoreWCF.Client.Tests.TestCases
{
    /// <summary>
    /// 配置文件测试
    /// </summary>
    [TestClass]
    public class ConfigurationTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            ServiceModelSectionGroup.Initialize(configuration);
        }
        #endregion

        #region # 测试配置节点 —— void TestSections()
        /// <summary>
        /// 测试配置节点
        /// </summary>
        [TestMethod]
        public void TestSections()
        {
            foreach (ConfigurationSection configurationSection in ServiceModelSectionGroup.Setting.Sections)
            {
                Trace.WriteLine(configurationSection);
            }
            Trace.WriteLine("------------------------------");
        }
        #endregion

        #region # 测试终结点 —— void TestEndpoints()
        /// <summary>
        /// 测试终结点
        /// </summary>
        [TestMethod]
        public void TestEndpoints()
        {
            foreach (ChannelEndpointElement endpoint in ServiceModelSectionGroup.Setting.Clients.Endpoints)
            {
                Trace.WriteLine(endpoint.Address);
                Trace.WriteLine(endpoint.Binding);
                Trace.WriteLine(endpoint.BindingConfiguration);
                Trace.WriteLine(endpoint.Contract);
                Trace.WriteLine(endpoint.Name);
                Trace.WriteLine(endpoint.BehaviorConfiguration);
                Trace.WriteLine("------------------------------");
            }
        }
        #endregion

        #region # 测试绑定 —— void TestBindings()
        /// <summary>
        /// 测试绑定
        /// </summary>
        [TestMethod]
        public void TestBindings()
        {
            Trace.WriteLine("BasicHttpBinding配置");
            foreach (BasicHttpBindingElement basicHttpBindingElement in ServiceModelSectionGroup.Setting.Bindings.BasicHttpBinding.Bindings)
            {
                Trace.WriteLine("---------------绑定节点配置---------------");

                Trace.WriteLine(basicHttpBindingElement.Name);
                Trace.WriteLine(basicHttpBindingElement.MaxBufferPoolSize);
                Trace.WriteLine(basicHttpBindingElement.MaxBufferSize);
                Trace.WriteLine(basicHttpBindingElement.MaxReceivedMessageSize);
                Trace.WriteLine(basicHttpBindingElement.CloseTimeout);
                Trace.WriteLine(basicHttpBindingElement.OpenTimeout);
                Trace.WriteLine(basicHttpBindingElement.ReceiveTimeout);
                Trace.WriteLine(basicHttpBindingElement.SendTimeout);
                Trace.WriteLine(basicHttpBindingElement.Security.Mode);
                Trace.WriteLine(basicHttpBindingElement.Security.Message.ClientCredentialType);
                Trace.WriteLine(basicHttpBindingElement.Security.Transport.ClientCredentialType);

                Trace.WriteLine("---------------绑定配置---------------");

                Binding binding = basicHttpBindingElement.CreateBinding();
                Trace.WriteLine(binding.Name);
                Trace.WriteLine(binding.CloseTimeout);
                Trace.WriteLine(binding.OpenTimeout);
                Trace.WriteLine(binding.ReceiveTimeout);
                Trace.WriteLine(binding.SendTimeout);

                Trace.WriteLine("------------------------------");
            }

            Trace.WriteLine("NetTcpBinding配置");
            foreach (NetTcpBindingElement netTcpBindingElement in ServiceModelSectionGroup.Setting.Bindings.NetTcpBinding.Bindings)
            {
                Trace.WriteLine("---------------绑定节点配置---------------");

                Trace.WriteLine(netTcpBindingElement.Name);
                Trace.WriteLine(netTcpBindingElement.MaxBufferPoolSize);
                Trace.WriteLine(netTcpBindingElement.MaxBufferSize);
                Trace.WriteLine(netTcpBindingElement.MaxReceivedMessageSize);
                Trace.WriteLine(netTcpBindingElement.CloseTimeout);
                Trace.WriteLine(netTcpBindingElement.OpenTimeout);
                Trace.WriteLine(netTcpBindingElement.ReceiveTimeout);
                Trace.WriteLine(netTcpBindingElement.SendTimeout);
                Trace.WriteLine(netTcpBindingElement.Security.Mode);
                Trace.WriteLine(netTcpBindingElement.Security.Message.ClientCredentialType);
                Trace.WriteLine(netTcpBindingElement.Security.Transport.ClientCredentialType);

                Trace.WriteLine("---------------绑定配置---------------");

                Binding binding = netTcpBindingElement.CreateBinding();
                Trace.WriteLine(binding.Name);
                Trace.WriteLine(binding.CloseTimeout);
                Trace.WriteLine(binding.OpenTimeout);
                Trace.WriteLine(binding.ReceiveTimeout);
                Trace.WriteLine(binding.SendTimeout);

                Trace.WriteLine("------------------------------");
            }

            Trace.WriteLine("BasicHttpBinding配置");
            foreach (WSHttpBindingElement wsHttpBindingElement in ServiceModelSectionGroup.Setting.Bindings.WSHttpBinding.Bindings)
            {
                Trace.WriteLine("---------------绑定节点配置---------------");

                Trace.WriteLine(wsHttpBindingElement.Name);
                Trace.WriteLine(wsHttpBindingElement.MaxBufferPoolSize);
                Trace.WriteLine(wsHttpBindingElement.MaxReceivedMessageSize);
                Trace.WriteLine(wsHttpBindingElement.CloseTimeout);
                Trace.WriteLine(wsHttpBindingElement.OpenTimeout);
                Trace.WriteLine(wsHttpBindingElement.ReceiveTimeout);
                Trace.WriteLine(wsHttpBindingElement.SendTimeout);
                Trace.WriteLine(wsHttpBindingElement.Security.Mode);
                Trace.WriteLine(wsHttpBindingElement.Security.Message.ClientCredentialType);
                Trace.WriteLine(wsHttpBindingElement.Security.Transport.ClientCredentialType);

                Trace.WriteLine("---------------绑定配置---------------");

                Binding binding = wsHttpBindingElement.CreateBinding();
                Trace.WriteLine(binding.Name);
                Trace.WriteLine(binding.CloseTimeout);
                Trace.WriteLine(binding.OpenTimeout);
                Trace.WriteLine(binding.ReceiveTimeout);
                Trace.WriteLine(binding.SendTimeout);

                Trace.WriteLine("------------------------------");
            }
        }
        #endregion

        #region # 测试行为 —— void TestBehaviors()
        /// <summary>
        /// 测试行为
        /// </summary>
        [TestMethod]
        public void TestBehaviors()
        {
            Trace.WriteLine("终结点行为配置");
            Trace.WriteLine("------------------------------");
            foreach (BehaviorConfigurationElement behaviorConfigurationElement in ServiceModelSectionGroup.Setting.Behaviors.BehaviorConfigurations)
            {
                Trace.WriteLine(behaviorConfigurationElement.Name);
                foreach (EndpointBehaviorElement endpointBehavior in behaviorConfigurationElement.EndpointBehaviors)
                {
                    Trace.WriteLine(endpointBehavior.Assembly);
                    Trace.WriteLine(endpointBehavior.Type);
                }
                Trace.WriteLine("------------------------------");
            }
        }
        #endregion
    }
}
