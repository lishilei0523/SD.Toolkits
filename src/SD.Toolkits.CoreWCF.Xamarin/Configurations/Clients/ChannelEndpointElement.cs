using SD.Toolkits.CoreWCF.Xamarin.Configurations.Commons;
using System;
using System.Configuration;

namespace SD.Toolkits.CoreWCF.Xamarin.Configurations.Clients
{
    /// <summary>
    /// �ŵ��ս��ڵ�
    /// </summary>
    public sealed class ChannelEndpointElement : ConfigurationElement
    {
        #region # ��ַ ���� Uri Address
        /// <summary>
        /// ��ַ
        /// </summary>
        [ConfigurationProperty(ConfigurationStrings.Address, Options = ConfigurationPropertyOptions.None)]
        public Uri Address
        {
            get { return (Uri)base[ConfigurationStrings.Address]; }
            set { base[ConfigurationStrings.Address] = value; }
        }
        #endregion

        #region # �� ���� string Binding
        /// <summary>
        /// ��
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

        #region # ������ ���� string BindingConfiguration
        /// <summary>
        /// ������
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

        #region # ��Լ ���� string Contract
        /// <summary>
        /// ��Լ
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

        #region # ���� ���� string Name
        /// <summary>
        /// ����
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

        #region # ��Ϊ���� ���� string BehaviorConfiguration
        /// <summary>
        /// ��Ϊ����
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
