using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace SD.Toolkits.SerialNumber.Tests.TestCases
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            SerialNumberSection.Initialize(configuration);
#endif
        }

        [TestMethod]
        public void TestConfigurations()
        {
            Trace.WriteLine(SerialNumberSection.Setting.SerialSeedProvider.Assembly);
            Trace.WriteLine(SerialNumberSection.Setting.SerialSeedProvider.Type);
            Trace.WriteLine(SerialNumberSection.Setting.ConnectionString.Name);
            Trace.WriteLine(SerialNumberSection.Setting.ConnectionString.Value);
        }
    }
}
