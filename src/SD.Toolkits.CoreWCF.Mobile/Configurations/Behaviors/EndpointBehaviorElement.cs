using System;
using System.Configuration;
using System.Reflection;
using System.ServiceModel.Description;

namespace SD.Toolkits.CoreWCF.Mobile.Configurations.Behaviors
{
    /// <summary>
    /// 终节点行为节点
    /// </summary>
    public class EndpointBehaviorElement : ConfigurationElement
    {
        #region # 类型 —— string Type
        /// <summary>
        /// 类型
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }
        #endregion

        #region # 程序集 —— string Assembly
        /// <summary>
        /// 程序集
        /// </summary>
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }
        #endregion

        #region # 创建终结点行为 —— IEndpointBehavior CreateEndpointBehavior()
        /// <summary>
        /// 创建终结点行为
        /// </summary>
        /// <returns>终结点行为</returns>
        public IEndpointBehavior CreateEndpointBehavior()
        {
            Assembly assembly = System.Reflection.Assembly.Load(this.Assembly);
            Type type = assembly.GetType(this.Type);

            #region # 验证

            if (!typeof(IEndpointBehavior).IsAssignableFrom(type))
            {
                throw new InvalidOperationException($"类型\"{type.FullName}\"未实现接口\"{nameof(IEndpointBehavior)}\"！");
            }

            #endregion

            IEndpointBehavior endpointBehavior = (IEndpointBehavior)Activator.CreateInstance(type);

            return endpointBehavior;
        }
        #endregion
    }
}
