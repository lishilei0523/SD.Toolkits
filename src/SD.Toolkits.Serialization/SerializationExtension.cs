using MessagePack;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SD.Toolkits.Serialization
{
    /// <summary>
    /// 序列化扩展
    /// </summary>
    public static class SerializationExtension
    {
        #region # 序列化字节数组 —— static byte[] ToBytes(this object instance)
        /// <summary>
        /// 序列化字节数组
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return Array.Empty<byte>();
            }

            #endregion

            try
            {
                byte[] bytes = MessagePackSerializer.Serialize(instance);

                return bytes;
            }
            catch (FormatterNotRegisteredException)
            {
                throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"MessagePackObject\"或\"DataContract\"特性！");
            }
        }
        #endregion

        #region # 序列化Base64文本 —— static string ToBase64String(this object instance)
        /// <summary>
        /// 序列化Base64文本
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns>Base64文本</returns>
        public static string ToBase64String(this object instance)
        {
            #region # 验证

            if (instance == null)
            {
                return null;
            }

            #endregion

            try
            {
                byte[] bytes = MessagePackSerializer.Serialize(instance);
                string base64String = Convert.ToBase64String(bytes);

                return base64String;
            }
            catch (FormatterNotRegisteredException)
            {
                throw new SerializationException($"给定对象类型\"{instance.GetType().Name}\"未标记\"MessagePackObject\"或\"DataContract\"特性！");
            }
        }
        #endregion

        #region # 字节数组反序列化实例 —— static T AsBytesTo<T>(this byte[] bytes)
        /// <summary>
        /// 字节数组反序列化实例
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>实例</returns>
        public static T AsBytesTo<T>(this byte[] bytes)
        {
            #region # 验证

            if (bytes == null || !bytes.Any())
            {
                return default;
            }

            #endregion

            try
            {
                T instance = MessagePackSerializer.Deserialize<T>(bytes); ;

                return instance;
            }
            catch (FormatterNotRegisteredException)
            {
                throw new SerializationException($"给定对象类型\"{typeof(T).Name}\"未标记\"MessagePackObject\"或\"DataContract\"特性！");
            }
        }
        #endregion

        #region # Base64文本反序列化实例 —— static T AsBase64StringTo<T>(this string base64String)
        /// <summary>
        /// Base64文本反序列化实例
        /// </summary>
        /// <param name="base64String">Base64文本</param>
        /// <returns>实例</returns>
        public static T AsBase64StringTo<T>(this string base64String)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(base64String))
            {
                return default;
            }

            #endregion

            byte[] bytes = Convert.FromBase64String(base64String);
            try
            {
                T instance = MessagePackSerializer.Deserialize<T>(bytes); ;

                return instance;
            }
            catch (FormatterNotRegisteredException)
            {
                throw new SerializationException($"给定对象类型\"{typeof(T).Name}\"未标记\"MessagePackObject\"或\"DataContract\"特性！");
            }
        }
        #endregion

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

            byte[] bytes = instance.ToBytes();
            T copy = bytes.AsBytesTo<T>();

            return copy;
        }
        #endregion
    }
}
