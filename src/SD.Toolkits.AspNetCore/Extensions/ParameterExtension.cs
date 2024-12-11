using Newtonsoft.Json;
using System;

namespace SD.Toolkits.AspNetCore.Extensions
{
    /// <summary>
    /// 参数扩展
    /// </summary>
    public static class ParameterExtension
    {
        #region # 类型化参数 —— static object TypifyParameterValue(Type parameterType, string parameterValue...
        /// <summary>
        /// 类型化参数
        /// </summary>
        /// <param name="parameterType">参数类型</param>
        /// <param name="parameterValue">参数值</param>
        /// <param name="jsonSerializerSettings">JSON序列化设置</param>
        /// <returns>类型化参数值</returns>
        public static object TypifyParameterValue(Type parameterType, string parameterValue, JsonSerializerSettings jsonSerializerSettings)
        {
            if (!string.IsNullOrWhiteSpace(parameterValue))
            {
                object typicalValue;
                if (parameterType == typeof(string))
                {
                    typicalValue = parameterValue;
                }
                else if (parameterType == typeof(Guid))
                {
                    typicalValue = Guid.Parse(parameterValue);
                }
                else if (parameterType == typeof(DateTime))
                {
                    typicalValue = DateTime.Parse(parameterValue);
                }
                else if (parameterType == typeof(TimeSpan))
                {
                    typicalValue = TimeSpan.Parse(parameterValue);
                }
                else if (parameterType.IsEnum)
                {
                    typicalValue = Enum.Parse(parameterType, parameterValue);
                }
                else if (parameterType.IsPrimitive)
                {
                    typicalValue = Convert.ChangeType(parameterValue, parameterType);
                }
                else if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type originalType = parameterType.GetGenericArguments()[0];
                    return TypifyParameterValue(originalType, parameterValue, jsonSerializerSettings);
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型外，都按对象反序列化
                    typicalValue = JsonConvert.DeserializeObject(parameterValue, parameterType, jsonSerializerSettings);
                }

                return typicalValue;
            }

            return null;
        }
        #endregion

        #region # 类型化参数 —— static object TypifyParameterValue(Type parameterType, object parameterValue...
        /// <summary>
        /// 类型化参数
        /// </summary>
        /// <param name="parameterType">参数类型</param>
        /// <param name="parameterValue">参数值</param>
        /// <param name="jsonSerializerSettings">JSON序列化设置</param>
        /// <returns>类型化参数值</returns>
        public static object TypifyParameterValue(Type parameterType, object parameterValue, JsonSerializerSettings jsonSerializerSettings)
        {
            if (parameterValue != null)
            {
                object typicalValue;
                if (parameterType == typeof(string))
                {
                    typicalValue = parameterValue.ToString();
                }
                else if (parameterType == typeof(Guid))
                {
                    typicalValue = Guid.Parse(parameterValue.ToString()!);
                }
                else if (parameterType == typeof(DateTime))
                {
                    typicalValue = DateTime.Parse(parameterValue.ToString()!);
                }
                else if (parameterType == typeof(TimeSpan))
                {
                    typicalValue = TimeSpan.Parse(parameterValue.ToString()!);
                }
                else if (parameterType.IsEnum)
                {
                    typicalValue = Enum.Parse(parameterType, parameterValue.ToString()!);
                }
                else if (parameterType.IsPrimitive)
                {
                    typicalValue = Convert.ChangeType(parameterValue, parameterType);
                }
                else if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type originalType = parameterType.GetGenericArguments()[0];
                    return TypifyParameterValue(originalType, parameterValue, jsonSerializerSettings);
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型外，都按对象反序列化
                    typicalValue = JsonConvert.DeserializeObject(parameterValue.ToString()!, parameterType, jsonSerializerSettings);
                }

                return typicalValue;
            }

            return null;
        }
        #endregion
    }
}
