using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Xamarin.Configurations.Clients
{
    /// <summary>
    /// 信道终结点节点
    /// </summary>
    public sealed class ChannelEndpointElement : ConfigurationElement
    {
        #region # 地址 —— Uri Address
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.Address, Options = ConfigurationPropertyOptions.None)]
        public Uri Address
        {
            get { return (Uri)base[ConfigurationStrings.Address]; }
            set { base[ConfigurationStrings.Address] = value; }
        }
        #endregion

        #region # 绑定 —— string Binding
        /// <summary>
        /// 绑定
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.Binding, DefaultValue = "")]
        [StringValidator(MinLength = 0)]
        public string Binding
        {
            get { return (string)base[ConfigurationStrings.Binding]; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Empty;
                }
                base[ConfigurationStrings.Binding] = value;
            }
        }
        #endregion

        #region # 绑定配置 —— string BindingConfiguration
        /// <summary>
        /// 绑定配置
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.BindingConfiguration, DefaultValue = "")]
        [StringValidator(MinLength = 0)]
        public string BindingConfiguration
        {
            get { return (string)base[ConfigurationStrings.BindingConfiguration]; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Empty;
                }
                base[ConfigurationStrings.BindingConfiguration] = value;
            }
        }
        #endregion

        #region # 契约 —— string Contract
        /// <summary>
        /// 契约
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.Contract, DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
        [StringValidator(MinLength = 0)]
        public string Contract
        {
            get { return (string)base[ConfigurationStrings.Contract]; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    value = String.Empty;
                }
                base[ConfigurationStrings.Contract] = value;
            }
        }
        #endregion

        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.Name, DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
        [StringValidator(MinLength = 0)]
        public string Name
        {
            get { return (string)base[ConfigurationStrings.Name]; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Empty;
                }
                base[ConfigurationStrings.Name] = value;
            }
        }
        #endregion

        #region # 行为配置 —— string BehaviorConfiguration
        /// <summary>
        /// 行为配置
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.BehaviorConfiguration, DefaultValue = "")]
        [StringValidator(MinLength = 0)]
        public string BehaviorConfiguration
        {
            get { return (string)base[ConfigurationStrings.BehaviorConfiguration]; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = String.Empty;
                }
                base[ConfigurationStrings.BehaviorConfiguration] = value;
            }
        }
        #endregion
    }
}
