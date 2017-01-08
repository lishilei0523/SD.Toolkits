using System;
using System.IO;
using System.Xml.Serialization;

namespace SD.Toolkits.Text.Xml
{
    /// <summary>
    /// Xml扩展方法
    /// </summary>
    public static class Extension
    {
        #region # object序列化Xml字符串扩展方法 —— static string ToXml(this object instance)
        /// <summary>
        /// object序列化Xml字符串扩展方法
        /// </summary>
        /// <param name="instance">object及其子类实例</param>
        /// <returns>Xml字符串</returns>
        /// <exception cref="ArgumentNullException">源实例为空</exception>
        public static string ToXml(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("instance", @"源实例不可为空！");
            }

            #endregion

            using (TextWriter stringWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(instance.GetType());
                xmlSerializer.Serialize(stringWriter, instance);

                return stringWriter.ToString();
            }
        }
        #endregion

        #region # XML字符串反序列化为对象扩展方法 —— static T XmlToObject<T>(this string xml)
        /// <summary>
        /// XML字符串反序列化为对象扩展方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">Xml字符串</param>
        /// <returns>给定类型对象</returns>
        /// <exception cref="ArgumentNullException">Xml字符串为空</exception>
        public static T XmlToObject<T>(this string xml)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException("xml", @"Xml字符串不可为空！");
            }

            #endregion

            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                try
                {
                    return (T)xmlSerializer.Deserialize(stringReader);
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException(string.Format("无法将源Xml字符串反序列化为给定类型\"{0}\"，请检查类型后重试！", typeof(T).Name));
                }
            }
        }
        #endregion
    }
}
