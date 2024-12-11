using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SD.Common
{
    /// <summary>
    /// 序列化扩展
    /// </summary>
    public static class SerializationExtension
    {
        #region # 序列化XML文本 —— static string ToXml(this object instance)
        /// <summary>
        /// 序列化XML文本
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>XML文本</returns>
        public static string ToXml(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            using TextWriter textWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(instance.GetType());
            xmlSerializer.Serialize(textWriter, instance);
            string xml = textWriter.ToString();

            return xml;
        }
        #endregion

        #region # XML文本反序列化实例 —— static T AsXmlTo<T>(this string xml)
        /// <summary>
        /// XML文本反序列化实例
        /// </summary>
        /// <param name="xml">XML文本</param>
        /// <returns>实例</returns>
        public static T AsXmlTo<T>(this string xml)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(xml))
            {
                return default(T);
            }

            #endregion

            using StringReader stringReader = new StringReader(xml);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                T instance = (T)xmlSerializer.Deserialize(stringReader);

                return instance;
            }
            catch (InvalidCastException exception)
            {
                throw new InvalidCastException($"无法将源XML文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
            }
        }
        #endregion

        #region # XML字节数组反序列化实例 —— static T AsXmlTo<T>(this byte[] xmlBytes)
        /// <summary>
        /// XML字节数组反序列化实例
        /// </summary>
        /// <param name="xmlBytes">XML字节数组</param>
        /// <returns>实例</returns>
        public static T AsXmlTo<T>(this byte[] xmlBytes)
        {
            #region # 验证

            if (xmlBytes == null || !xmlBytes.Any())
            {
                return default(T);
            }

            #endregion

            using MemoryStream memoryStream = new MemoryStream(xmlBytes);
            using StreamReader streamReader = new StreamReader(memoryStream);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                T instance = (T)xmlSerializer.Deserialize(streamReader);

                return instance;
            }
            catch (InvalidCastException exception)
            {
                throw new InvalidCastException($"无法将源XML字节数组反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
            }
        }
        #endregion
    }
}
