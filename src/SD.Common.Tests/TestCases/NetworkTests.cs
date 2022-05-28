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
        /// <summary>
        /// 获取MAC测试
        /// </summary>
        [TestMethod]
        public void TestGetMacs()
        {
            IList<string> macs = NetworkExtension.GetMacs();
            Trace.WriteLine(macs);
        }

        /// <summary>
        /// 获取IP测试
        /// </summary>
        [TestMethod]
        public void TestGetIps()
        {
            IList<string> ips = NetworkExtension.GetIPs();
            Trace.WriteLine(ips);
        }
    }
}
