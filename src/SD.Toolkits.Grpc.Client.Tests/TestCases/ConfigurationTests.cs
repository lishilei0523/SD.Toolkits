using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.Grpc.Client.Configurations;
using System;
using System.Configuration;
using System.Reflection;

namespace SD.Toolkits.Grpc.Client.Tests.TestCases
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(assembly);
            GrpcSection.Initialize(configuration);
        }
        #endregion

        #region # 测试配置文件 —— void TestConfiguration()
        /// <summary>
        /// 测试配置文件
        /// </summary>
        [TestMethod]
        public void TestConfiguration()
        {
            foreach (EndpointElement endpoint in GrpcSection.Setting.EndpointElements)
            {
                Console.WriteLine(endpoint.Address);
                Console.WriteLine(endpoint.Contract);
                Console.WriteLine(endpoint.AuthInterceptors);
                Console.WriteLine(endpoint.EndpointConfiguration);
            }
            foreach (AuthInterceptorElement authInterceptor in GrpcSection.Setting.AuthInterceptorElements)
            {
                Console.WriteLine(authInterceptor.Name);
                Console.WriteLine(authInterceptor.Type);
                Console.WriteLine(authInterceptor.Assembly);
            }
            foreach (EndpointConfigurationElement endpointConfiguration in GrpcSection.Setting.EndpointConfigurationElements)
            {
                Console.WriteLine(endpointConfiguration.Name);
                Console.WriteLine(endpointConfiguration.MaxSendMessageSize);
                Console.WriteLine(endpointConfiguration.MaxReceiveMessageSize);
                Console.WriteLine(endpointConfiguration.MaxRetryAttempts);
                Console.WriteLine(endpointConfiguration.MaxRetryBufferSize);
                Console.WriteLine(endpointConfiguration.MaxRetryBufferPerCallSize);
                Console.WriteLine(endpointConfiguration.DisposeHttpClient);
                Console.WriteLine(endpointConfiguration.ThrowOperationCanceledOnCancellation);
            }
        }
        #endregion
    }
}
