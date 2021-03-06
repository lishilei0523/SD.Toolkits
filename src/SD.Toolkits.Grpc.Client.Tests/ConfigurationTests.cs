using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Grpc.Client.Configurations;
using System;

namespace SD.Toolkits.Grpc.Client.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
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
    }
}
