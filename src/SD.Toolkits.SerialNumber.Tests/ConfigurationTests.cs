using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.SerialNumber;
using System.Diagnostics;

namespace SD.Toolkits.NoGenerator.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
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
