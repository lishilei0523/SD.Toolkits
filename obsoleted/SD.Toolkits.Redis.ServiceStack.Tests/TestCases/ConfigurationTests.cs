using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Redis.Configuration;
using System.Diagnostics;

namespace SD.Toolkits.Redis.Tests.TestCases
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            foreach (ServerElement server in RedisConfiguration.Setting.ReadWriteServers)
            {
                Trace.WriteLine(server.Host);
                Trace.WriteLine(server.Port);
                Trace.WriteLine(server.Password);
            }

            foreach (ServerElement server in RedisConfiguration.Setting.ReadOnlyServers)
            {
                Trace.WriteLine(server.Host);
                Trace.WriteLine(server.Port);
                Trace.WriteLine(server.Password);
            }
        }
    }
}
