using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SD.Toolkits.Text.Binary
{
    /// <summary>
    /// 二进制扩展方法
    /// </summary>
    public static class Extension
    {
        #region # 静态字段及静态构造器

        /// <summary>
        /// 二进制序列化器
        /// </summary>
        private static readonly BinaryFormatter _BinaryFormatter;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Extension()
        {
            _BinaryFormatter = new BinaryFormatter();
        }

        #endregion

        #region # object序列化为二进制字符串扩展方法 —— static string ToBinaryString(this object instance)
        /// <summary>
        /// object序列化为二进制字符串扩展方法
        /// </summary>
        /// <param name="instance">object及其子类实例</param>
        /// <returns>二进制字符串</returns>
        /// <exception cref="ArgumentNullException">源实例为空</exception>
        /// <exception cref="SerializationException">实例类型未标记"Serializable"特性</exception>
        public static string ToBinaryString(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("instance", @"源实例不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    _BinaryFormatter.Serialize(stream, instance);
                    return Convert.ToBase64String(stream.ToArray());
                }
                catch (SerializationException ex)
                {
                    throw new SerializationException(string.Format("给定对象类型\"{0}\"未标记\"Serializable\"特性！", instance.GetType().Name), ex);
                }
            }
        }
        #endregion

        #region # 二进制字符串反序列化为对象扩展方法 —— static T BinaryToObject<T>(this string binaryStr)
        /// <summary>
        /// 二进制字符串反序列化为对象扩展方法
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="binaryStr">二进制字符串</param>
        /// <returns>给定类型对象</returns>
        /// <exception cref="ArgumentNullException">二进制字符串为空</exception>
        /// <exception cref="SerializationException">对象类型未标记"Serializable"特性</exception>
        /// <exception cref="InvalidCastException">反序列化为给定类型失败</exception>
        public static T BinaryToObject<T>(this string binaryStr)
        {
            #region # 验证参数

            if (string.IsNullOrWhiteSpace(binaryStr))
            {
                throw new ArgumentNullException("binaryStr", @"二进制字符串不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(binaryStr)))
            {
                try
                {
                    return (T)_BinaryFormatter.Deserialize(stream);
                }
                catch (SerializationException ex)
                {
                    throw new SerializationException(string.Format("给定对象类型\"{0}\"未标记\"Serializable\"特性！",
                        typeof(T).Name), ex);
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidCastException(string.Format("无法将源二进制字符串反序列化为给定类型\"{0}\"，请检查类型后重试！", typeof(T).Name), ex);
                }
            }
        }
        #endregion
    }
}
