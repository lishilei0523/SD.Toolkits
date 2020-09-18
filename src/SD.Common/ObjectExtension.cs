using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SD.Common
{
    /// <summary>
    /// object扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        #region # 静态字段及静态构造器

        /// <summary>
        /// 二进制序列化器
        /// </summary>
        private static readonly BinaryFormatter _BinaryFormatter;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ObjectExtension()
        {
            _BinaryFormatter = new BinaryFormatter();
        }

        #endregion

        #region # 对象属性赋值 —— static void Fill<TSource, TTarget>(this TSource...
        /// <summary>
        /// 对象属性赋值
        /// </summary>
        /// <typeparam name="TSource">源实例类型</typeparam>
        /// <typeparam name="TTarget">目标实例类型</typeparam>
        /// <param name="sourceInstance">源实例</param>
        /// <param name="targetInstance">目标实例</param>
        public static void Fill<TSource, TTarget>(this TSource sourceInstance, TTarget targetInstance)
        {
            //01.获取源对象与目标对象的类型
            Type sourceType = sourceInstance.GetType();
            Type targetType = targetInstance.GetType();

            //02.获取源对象与目标对象的所有属性
            PropertyInfo[] sourceProps = sourceType.GetProperties();
            PropertyInfo[] targetProps = targetType.GetProperties();

            //03.双重遍历，判断属性名称是否相同，如果相同则赋值
            foreach (PropertyInfo tgtProp in targetProps)
            {
                foreach (PropertyInfo srcProp in sourceProps)
                {
                    if (tgtProp.Name == srcProp.Name)
                    {
                        tgtProp.SetValue(targetInstance, srcProp.GetValue(sourceInstance, null), null);
                    }
                }
            }
        }
        #endregion

        #region # object原型深拷贝扩展方法 —— static T Clone<T>(this object instance)
        /// <summary>
        /// object原型深拷贝扩展方法
        /// 异常：ArgumentNullException，NullReferenceException，SerializationException
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">object及其子类对象</param>
        /// <returns>给定类型对象</returns>
        /// <exception cref="ArgumentNullException">源对象为空</exception>
        /// <exception cref="InvalidCastException">反序列化为给定类型失败</exception>
        /// <exception cref="SerializationException">对象类型未标记"Serializable"特性</exception>
        public static T Clone<T>(this object instance) where T : class
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("obj", @"源对象不可为空！");
            }

            #endregion

            using (Stream stream = new MemoryStream())
            {
                _BinaryFormatter.Serialize(stream, instance);
                stream.Position = 0;
                T ectype = _BinaryFormatter.Deserialize(stream) as T;

                #region # 非空验证

                if (ectype == null)
                {
                    throw new InvalidCastException(string.Format("无法将源类型\"{0}\"反序列化为给定类型\"{1}\"，请检查类型后重试！", instance.GetType().Name, typeof(T).Name));
                }

                #endregion

                return ectype;
            }
        }
        #endregion

        #region # object序列化为二进制字符串扩展方法 —— static string ToBinaryString(this object instance)
        /// <summary>
        /// object序列化为二进制字符串扩展方法
        /// </summary>
        /// <param name="instance">object及其子类</param>
        /// <returns>二进制字符串</returns>
        /// <exception cref="ArgumentNullException">源对象为空</exception>
        /// <exception cref="SerializationException">对象类型未标记"Serializable"特性</exception>
        public static string ToBinaryString(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("instance", @"源对象不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    _BinaryFormatter.Serialize(stream, instance);
                    return Convert.ToBase64String(stream.ToArray());
                }
                catch (SerializationException)
                {
                    throw new SerializationException(string.Format("给定对象类型\"{0}\"未标记\"Serializable\"特性！", instance.GetType().Name));
                }
            }
        }
        #endregion

        #region # object序列化为byte数组扩展方法 —— static byte[] ToByteArray(this object instance)
        /// <summary>
        /// object序列化为byte数组扩展方法
        /// </summary>
        /// <param name="instance">object及其子类</param>
        /// <returns>byte数组</returns>
        /// <exception cref="ArgumentNullException">源对象为空</exception>
        /// <exception cref="SerializationException">对象类型未标记"Serializable"特性</exception>
        public static byte[] ToByteArray(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("instance", @"源对象不可为空！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    _BinaryFormatter.Serialize(stream, instance);
                    return stream.ToArray();
                }
                catch (SerializationException)
                {
                    throw new SerializationException(string.Format("给定对象类型\"{0}\"未标记\"Serializable\"特性！", instance.GetType().Name));
                }
            }
        }
        #endregion

        #region # byte数组反序列化为对象扩展方法 —— static T ToObject<T>(this byte[] buffer)
        /// <summary>
        /// byte数组反序列化为对象扩展方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="buffer">byte数组</param>
        /// <returns>对象</returns>
        public static T ToObject<T>(this byte[] buffer)
        {
            #region # 验证参数

            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", @"byte数组不可为null！");
            }

            #endregion

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                object instance = _BinaryFormatter.Deserialize(stream);
                return (T)instance;
            }
        }
        #endregion

        #region # object序列化Xml字符串扩展方法 —— static string ToXml(this object instance)
        /// <summary>
        /// object序列化Xml字符串扩展方法
        /// </summary>
        /// <param name="instance">object及其子类对象</param>
        /// <returns>Xml字符串</returns>
        /// <exception cref="ArgumentNullException">源对象为空</exception>
        public static string ToXml(this object instance)
        {
            #region # 验证参数

            if (instance == null)
            {
                throw new ArgumentNullException("obj", @"源对象不可为空！");
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

        #region # .NET类型值转数据库类型值空值处理 —— static object ToDbValue(this object value)
        /// <summary>
        /// .NET类型值转数据库类型值空值处理
        /// </summary>
        /// <param name="value">.NET类型值</param>
        /// <returns>处理后的数据库类型值</returns>
        public static object ToDbValue(this object value)
        {
            return value ?? DBNull.Value;
        }
        #endregion
    }
}
