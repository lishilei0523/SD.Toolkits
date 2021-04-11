using System.Configuration;
using System.Xml;

namespace SD.Toolkits.AspNetCore.Configurations
{
    /// <summary>
    /// 路径节点
    /// </summary>
    public class PathElement : ConfigurationElement
    {
        /// <summary>
        /// 路径
        /// </summary>
        [ConfigurationProperty("data", IsRequired = true)]
        public string Path
        {
            get { return (string)this["data"]; }
            set { this["data"] = value; }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            this.Path = (string)reader.ReadElementContentAs(typeof(string), null);
        }
    }
}
