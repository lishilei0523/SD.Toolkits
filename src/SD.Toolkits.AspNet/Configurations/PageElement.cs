using System.Configuration;
using System.Xml;

namespace SD.Toolkits.AspNet.Configurations
{
    /// <summary>
    /// 页节点
    /// </summary>
    public class PageElement : ConfigurationElement
    {
        /// <summary>
        /// 链接地址
        /// </summary>
        [ConfigurationProperty("data", IsRequired = true)]
        public string Url
        {
            get { return (string)this["data"]; }
            set { this["data"] = value; }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            this.Url = (string)reader.ReadElementContentAs(typeof(string), null);
        }
    }
}
