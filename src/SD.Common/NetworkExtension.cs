using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SD.Common
{
    /// <summary>
    /// 网络扩展
    /// </summary>
    public static class NetworkExtension
    {
        #region # 获取本机MAC地址列表 —— static string GetLocalMacAddress()
        /// <summary>
        /// 获取本机MAC地址列表
        /// </summary>
        /// <returns>本机MAC地址列表</returns>
        /// <remarks>以“,”分隔</remarks>
        public static string GetLocalMacAddress()
        {
            IList<string> macs = GetMacs();
            string macsText = macs.ToSplicString();

            return macsText;
        }
        #endregion

        #region # 获取本机MAC地址列表 —— IList<string> GetMacs()
        /// <summary>
        /// 获取本机MAC地址列表
        /// </summary>
        /// <returns>本机MAC地址列表</returns>
        /// <remarks>以“,”分隔</remarks>
        public static IList<string> GetMacs()
        {
            ICollection<string> macs = new HashSet<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in interfaces)
            {
                PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                byte[] addressBytes = physicalAddress.GetAddressBytes();
                string mac = BitConverter.ToString(addressBytes);
                macs.Add(mac);
            }

            return macs.ToList();
        }
        #endregion

        #region # 获取本机IP地址列表 —— static string GetLocalIPAddress()
        /// <summary>
        /// 获取本机IP地址列表
        /// </summary>
        /// <returns>本机IP地址列表</returns>
        /// <remarks>以“,”分隔</remarks>
        public static string GetLocalIPAddress()
        {
            ICollection<string> ips = GetIPs();
            string ipsStr = ips.ToSplicString();

            return ipsStr;
        }
        #endregion

        #region # 获取本机IP地址列表 —— static IList<string> GetIPs()
        /// <summary>
        /// 获取本机本机IP地址
        /// </summary>
        /// <returns>本机IP地址列表</returns>
        public static IList<string> GetIPs()
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

            return ips.ToList();
        }
        #endregion
    }
}
