using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.AspNet.Configurations;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace SD.Toolkits.AspNet.Tests.TestCases
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly assembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(assembly);
            AspNetSection.Initialize(configuration);
#endif
        }

        [TestMethod]
        public void TestConfiguration()
        {
            foreach (HostElement hostElement in AspNetSection.Setting.HostElements)
            {
                Trace.WriteLine(hostElement.Port);
                Trace.WriteLine(hostElement.Protocol);
            }

            Trace.WriteLine(AspNetSection.Setting.ApplicationName.Value);
            Trace.WriteLine(AspNetSection.Setting.MachineKey.Value);
            Trace.WriteLine(AspNetSection.Setting.LoginPage.Value);
            Trace.WriteLine(AspNetSection.Setting.ErrorPage.Value);
            Trace.WriteLine(AspNetSection.Setting.StaticFiles.Value);
            Trace.WriteLine(AspNetSection.Setting.FileServer.Value);
            Trace.WriteLine(AspNetSection.Setting.X509.Path);
            Trace.WriteLine(AspNetSection.Setting.X509.Password);
        }
    }
}
