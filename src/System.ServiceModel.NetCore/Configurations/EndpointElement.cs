using System.Configuration;

namespace System.ServiceModel.NetCore.Configurations
{
    /// <summary>
    /// 终节点元素
    /// </summary>
    public class EndpointElement : ConfigurationElement
    {
        #region # 地址 —— string Address
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }
        #endregion

        #region # 绑定 —— string Binding
        /// <summary>
        /// 绑定
        /// </summary>
        [ConfigurationProperty("binding", IsRequired = true)]
        public string Binding
        {
            get { return (string)this["binding"]; }
            set { this["binding"] = value; }
        }
        #endregion

        #region # 契约 —— string Contract
        /// <summary>
        /// 契约
        /// </summary>
        [ConfigurationProperty("contract", IsRequired = true)]
        public string Contract
        {
            get { return (string)this["contract"]; }
            set { this["contract"] = value; }
        }
        #endregion

        #region # 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        #endregion

        #region # 行为配置 —— string BehaviorConfiguration
        /// <summary>
        /// 行为配置
        /// </summary>
        [ConfigurationProperty("behaviorConfiguration", IsRequired = false)]
        public string BehaviorConfiguration
        {
            get { return (string)this["behaviorConfiguration"]; }
            set { this["behaviorConfiguration"] = value; }
        }
        #endregion
    }
}
