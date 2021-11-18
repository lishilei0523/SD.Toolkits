using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SD.Common
{
    /// <summary>
    /// 序列化扩展
    /// </summary>
    public static class SerializationExtension
    {
        #region # 深拷贝实例 —— static T Clone<T>(this object instance)
        /// <summary>
        /// 深拷贝实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>实例副本</returns>
        public static T Clone<T>(this object instance) where T : class
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, instance);
                memoryStream.Position = 0;
                T copy = binaryFormatter.Deserialize(memoryStream) as T;

                #region # 验证

                if (copy == null)
                {
                    throw new InvalidCastException($"无法将源类型\"{instance.GetType().Name}\"反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！");
                }

                #endregion

                return copy;
            }
        }
        #endregion

        #region # 序列化二进制文本 —— static string ToBinaryString(this object instance)
        /// <summary>
        /// 序列化二进制文本
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>二进制文本</returns>
        public static string ToBinaryString(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, instance);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"Serializable\"特性！");
                }
            }
        }
        #endregion

        #region # 序列化二进制字节数组 —— static byte[] ToByteArray(this object instance)
        /// <summary>
        /// 序列化二进制字节数组
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>二进制字节数组</returns>
        public static byte[] ToByteArray(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return new byte[0];
            }

            #endregion

            using (MemoryStream memoryStream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, instance);
                    return memoryStream.ToArray();
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"Serializable\"特性！");
                }
            }
        }
        #endregion

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

            using (TextWriter stringWriter = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(instance.GetType());
                xmlSerializer.Serialize(stringWriter, instance);
                return stringWriter.ToString();
            }
        }
        #endregion

        #region # 二进制文本反序列化对象 —— static T AsBinaryTo<T>(this string binaryText)
        /// <summary>
        /// 二进制文本反序列化对象
        /// </summary>
        /// <param name="binaryText">二进制文本</param>
        /// <returns>实例</returns>
        public static T AsBinaryTo<T>(this string binaryText)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(binaryText))
            {
                return default(T);
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(binaryText)))
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{typeof(T).Name}\"未标记\"Serializable\"特性！");
                }
                catch (InvalidCastException exception)
                {
                    throw new InvalidCastException($"无法将给定二进制文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
                }
            }
        }
        #endregion

        #region # 二进制字节数组反序列化对象 —— static T AsBinaryTo<T>(this byte[] binaryBuffer)
        /// <summary>
        /// 二进制字节数组反序列化对象
        /// </summary>
        /// <param name="binaryBuffer">二进制字节数组</param>
        /// <returns>实例</returns>
        public static T AsBinaryTo<T>(this byte[] binaryBuffer)
        {
            #region # 验证

            if (binaryBuffer == null || !binaryBuffer.Any())
            {
                return default(T);
            }

            #endregion

            using (MemoryStream memoryStream = new MemoryStream(binaryBuffer))
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    object instance = binaryFormatter.Deserialize(memoryStream);
                    return (T)instance;
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{typeof(T).Name}\"未标记\"Serializable\"特性！");
                }
                catch (InvalidCastException exception)
                {
                    throw new InvalidCastException($"无法将给定byte数组反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
                }
            }
        }
        #endregion

        #region # XML文本反序列化对象 —— static T AsXmlTo<T>(this string xml)
        /// <summary>
        /// XML文本反序列化对象
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

            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                try
                {
                    return (T)xmlSerializer.Deserialize(stringReader);
                }
                catch (InvalidCastException exception)
                {
                    throw new InvalidCastException($"无法将源XML文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
                }
            }
        }
        #endregion

        #region # XML字节数组反序列化对象 —— static T AsXmlTo<T>(this byte[] xmlBuffer)
        /// <summary>
        /// XML字节数组反序列化对象
        /// </summary>
        /// <param name="xmlBuffer">XML字节数组</param>
        /// <returns>实例</returns>
        public static T AsXmlTo<T>(this byte[] xmlBuffer)
        {
            #region # 验证

            if (xmlBuffer == null || !xmlBuffer.Any())
            {
                return default(T);
            }

            #endregion

            using (MemoryStream memoryStream = new MemoryStream(xmlBuffer))
            {
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    try
                    {
                        return (T)xmlSerializer.Deserialize(streamReader);
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new InvalidCastException($"无法将源XML文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！", exception);
                    }
                }
            }
        }
        #endregion
    }
}
