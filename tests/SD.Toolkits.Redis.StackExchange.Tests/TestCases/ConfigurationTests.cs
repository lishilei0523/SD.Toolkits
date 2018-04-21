using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Redis.Configurations;
using System.Diagnostics;

namespace SD.Toolkits.Redis.Tests.TestCases
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string password = RedisSection.Setting.Password;
            Trace.WriteLine(password);

            foreach (EndpointElement endpoint in RedisSection.Setting.EndpointElement)
            {
                Trace.WriteLine(endpoint.Name);
                Trace.WriteLine(endpoint.Host);
                Trace.WriteLine(endpoint.Port);
            }
        }
    }
}
