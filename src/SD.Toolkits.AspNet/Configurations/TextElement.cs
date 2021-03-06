﻿using System.Configuration;
using System.Xml;

namespace SD.Toolkits.AspNet.Configurations
{
    /// <summary>
    /// 文本节点
    /// </summary>
    public class TextElement : ConfigurationElement
    {
        /// <summary>
        /// 值
        /// </summary>
        [ConfigurationProperty("data", IsRequired = true)]
        public string Value
        {
            get { return (string)this["data"]; }
            set { this["data"] = value; }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            this.Value = (string)reader.ReadElementContentAs(typeof(string), null);
        }
    }
}
