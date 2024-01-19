using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace SD.Common
{
    /// <summary>
    /// 配置扩展
    /// </summary>
    public static class ConfigurationExtension
    {
        #region # 从程序集获取嵌入配置 —— static Configuration GetConfigurationFromAssembly(...
        /// <summary>
        /// 从程序集获取嵌入配置
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>嵌入配置</returns>
        public static Configuration GetConfigurationFromAssembly(Assembly assembly)
        {
            #region # 验证

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly), "程序集不可为空！");
            }

            #endregion

            string assemblyName = assembly.GetName().Name;
            Stream configStream = assembly.GetManifestResourceStream($"{assemblyName}.App.config");

            #region # 验证

            if (configStream == null)
            {
                throw new NullReferenceException($"程序集\"{assemblyName}\"未嵌入App.config！");
            }

            #endregion

            using StreamReader streamReader = new StreamReader(configStream);
            string configContent = streamReader.ReadToEnd();
            using TemporaryFileStream temporaryFileStream = TemporaryFileStream.Create(configContent);
            ConfigurationFileMap configurationFileMap = new ConfigurationFileMap(temporaryFileStream.Name);
            Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(configurationFileMap);

            return configuration;
        }
        #endregion
    }
}
