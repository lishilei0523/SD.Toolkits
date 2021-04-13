using System;
using System.IO;
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

            using (Stream stream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, instance);
                stream.Position = 0;
                T copy = binaryFormatter.Deserialize(stream) as T;

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

        #region # 序列化二进制 —— static string ToBinary(this object instance)
        /// <summary>
        /// 序列化二进制
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>二进制文本</returns>
        public static string ToBinary(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, instance);
                    return Convert.ToBase64String(stream.ToArray());
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"Serializable\"特性！");
                }
            }
        }
        #endregion

        #region # 二进制反序列化对象 —— static T AsBinaryTo<T>(this string binary)
        /// <summary>
        /// 二进制反序列化对象
        /// </summary>
        /// <param name="binary">二进制文本</param>
        /// <returns>实例</returns>
        public static T AsBinaryTo<T>(this string binary)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(binary))
            {
                return default(T);
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(binary)))
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
                catch (InvalidCastException)
                {
                    throw new InvalidCastException($"无法将给定二进制文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！");
                }
            }
        }
        #endregion

        #region # 序列化XML —— static string ToXml(this object instance)
        /// <summary>
        /// 序列化XML
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

        #region # XML反序列化对象 —— static T AsXmlTo<T>(this string xml)
        /// <summary>
        /// XML反序列化对象
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
                catch (InvalidCastException)
                {
                    throw new InvalidCastException($"无法将源XML文本反序列化为给定类型\"{typeof(T).Name}\"，请检查类型后重试！");
                }
            }
        }
        #endregion

        #region # 序列化byte数组 —— static byte[] ToBuffer(this object instance)
        /// <summary>
        /// 序列化byte数组
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>byte数组</returns>
        public static byte[] ToBuffer(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return new byte[0];
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, instance);
                    return stream.ToArray();
                }
                catch (SerializationException)
                {
                    throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"Serializable\"特性！");
                }
            }
        }
        #endregion

        #region # byte数组反序列化对象 —— static T AsBufferTo<T>(this byte[] buffer)
        /// <summary>
        /// byte数组反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>实例</returns>
        public static T AsBufferTo<T>(this byte[] buffer)
        {
            #region # 验证

            if (buffer == null)
            {
                return default(T);
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                object instance = binaryFormatter.Deserialize(stream);
                return (T)instance;
            }
        }
        #endregion
    }
}
