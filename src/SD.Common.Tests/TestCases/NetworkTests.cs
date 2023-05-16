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
        #region # 获取IP测试 —— void TestGetIps()
        /// <summary>
        /// 获取IP测试
        /// </summary>
        [TestMethod]
        public void TestGetIps()
        {
            IList<string> ips = NetworkExtension.GetIPs();
            Trace.WriteLine(ips);
        }
        #endregion

        #region # 获取MAC测试 —— void TestGetMacs()
        /// <summary>
        /// 获取MAC测试
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
