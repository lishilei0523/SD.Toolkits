using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.Toolkits.AspNet.Configurations;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace SD.Toolkits.AspNet.Tests.TestCases
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
#if NETCOREAPP3_1_OR_GREATER
            Assembly assembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(assembly);
            AspNetSection.Initialize(configuration);
#endif
        }
        #endregion

        #region # 测试配置文件 —— void TestConfiguration()
        /// <summary>
        /// 测试配置文件
        /// </summary>
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
        #endregion
    }
}
