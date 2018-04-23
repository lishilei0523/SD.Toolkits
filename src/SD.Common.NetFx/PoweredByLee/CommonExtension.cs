using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SD.Common.PoweredByLee
{
    /// <summary>
    /// 常用扩展方法
    /// </summary>
    public static class CommonExtension
    {
        #region # 获取CPU唯一码列表 —— static ICollection<string> GetCpuIds()
        /// <summary>
        /// 获取CPU唯一码列表
        /// </summary>
        /// <returns>CPU唯一码列表</returns>
        public static ICollection<string> GetCpuIds()
        {
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection managementObjects = managementClass.GetInstances();

            ICollection<string> cpuIds = new HashSet<string>();

            foreach (ManagementBaseObject managementBase in managementObjects)
            {
                ManagementObject management = (ManagementObject)managementBase;
                PropertyData propertyData = management.Properties["ProcessorId"];
                object dataValue = propertyData.Value;

                if (dataValue != null)
                {
                    string cpuId = dataValue.ToString().Trim();
                    cpuIds.Add(cpuId);
                }
            }

            return cpuIds;
        }
        #endregion

        #region # 获取硬盘唯一码列表 —— static ICollection<string> GetHardDiskIds()
        /// <summary>
        /// 获取硬盘唯一码列表
        /// </summary>
        /// <returns>硬盘唯一码列表</returns>
        public static ICollection<string> GetHardDiskIds()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            ManagementObjectCollection managementObjects = searcher.Get();

            ICollection<string> hardDiskIds = new HashSet<string>();

            foreach (ManagementBaseObject managementBase in managementObjects)
            {
                ManagementObject management = (ManagementObject)managementBase;
                object serial = management["SerialNumber"];

                if (serial != null)
                {
                    string hardDiskId = serial.ToString().Trim();
                    hardDiskIds.Add(hardDiskId);
                }
            }

            return hardDiskIds;
        }
        #endregion

        #region # 获取MAC地址列表 —— static ICollection<string> GetMacAddresses()
        /// <summary>
        /// 获取MAC地址列表
        /// </summary>
        /// <returns>MAC地址列表</returns>
        public static ICollection<string> GetMacAddresses()
        {
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection managementObjects = managementClass.GetInstances();

            ICollection<string> macAddresses = new HashSet<string>();

            foreach (ManagementBaseObject managementBase in managementObjects)
            {
                ManagementObject management = (ManagementObject)managementBase;
                object mac = management["MacAddress"];

                if (mac != null)
                {
                    string macAddress = mac.ToString().Trim();
                    macAddresses.Add(macAddress);
                }
            }

            return macAddresses;
        }
        #endregion

        #region # 获取机器唯一码 —— static string GetMachineCode()
        /// <summary>
        /// 获取机器唯一码
        /// </summary>
        /// <returns>机器唯一码</returns>
        public static string GetMachineCode()
        {
            ICollection<string> hardDiskIds = GetHardDiskIds();
            ICollection<string> macs = GetMacAddresses();

            StringBuilder builder = new StringBuilder();

            if (hardDiskIds.Any())
            {
                builder.Append(hardDiskIds.First());
            }
            if (macs.Any())
            {
                builder.Append(macs.First());
            }

            string machineCode = builder.ToString().ToMD5();

            return machineCode;
        }
        #endregion


        //Private

        #region # 计算字符串MD5值扩展方法 —— static string ToMD5(this string text)
        /// <summary>
        /// 计算字符串MD5值扩展方法
        /// </summary>
        /// <param name="text">待转换的字符串</param>
        /// <returns>MD5值</returns>
        private static string ToMD5(this string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            using (MD5 md5 = MD5.Create())
            {
                buffer = md5.ComputeHash(buffer);
                StringBuilder md5Builder = new StringBuilder();
                foreach (byte @byte in buffer)
                {
                    md5Builder.Append(@byte.ToString("x2"));
                }
                return md5Builder.ToString();
            }
        }
        #endregion

    }
}
