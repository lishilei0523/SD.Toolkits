﻿using System.Xml.Serialization;

namespace SD.Toolkits.MonoAndroid.Models
{
    /// <summary>
    /// 发布
    /// </summary>
    [XmlRoot("item")]
    internal class Release
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// 是否强制
        /// </summary>
        [XmlElement("mandatory")]
        public bool Mandatory { get; set; }
    }
}
