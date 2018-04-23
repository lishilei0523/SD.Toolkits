using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace SD.Common.PoweredByLee
{
    /// <summary>
    /// 常用扩展方法
    /// </summary>
    public static class CommonExtension
    {
        #region # 获取本机IP地址 —— static string GetLocalIPAddress()
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>本机IP</returns>
        public static string GetLocalIPAddress()
        {
            ICollection<string> ips = GetIPs();
            string ipsStr = ips.ToSplicString();

            return ipsStr;
        }
        #endregion

        #region # 获取IP地址列表 —— static ICollection<string> GetIPs()
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>本机IP</returns>
        public static ICollection<string> GetIPs()
        {
            ICollection<string> ips = new HashSet<string>();

            string hostName = Dns.GetHostName();//本机名   

            ips.Add(hostName);

            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ipAddress.ToString());
                }
            }

            return ips;
        }
        #endregion
    }
}
