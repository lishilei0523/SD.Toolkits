using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SD.Common.Tests.TestCases
{
    /// <summary>
    /// 网络测试
    /// </summary>
    [TestClass]
    public class NetworkTests
    {
        #region # 测试获取IP —— void TestGetIps()
        /// <summary>
        /// 测试获取IP
        /// </summary>
        [TestMethod]
        public void TestGetIps()
        {
            IList<string> ips = NetworkExtension.GetIPs();
            Trace.WriteLine(ips);
        }
        #endregion

        #region # 测试获取MAC —— void TestGetMacs()
        /// <summary>
        /// 测试获取MAC
        /// </summary>
        [TestMethod]
        public void TestGetMacs()
        {
            IList<string> macs = NetworkExtension.GetMacs();
            Trace.WriteLine(macs);
        }
        #endregion
    }
}
