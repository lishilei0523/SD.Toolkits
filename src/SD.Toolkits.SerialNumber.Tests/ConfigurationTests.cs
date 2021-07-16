using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace SD.Toolkits.SerialNumber.Tests
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
