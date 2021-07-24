using System;
using System.Text.Json;

namespace SD.Toolkits.WebApiCore.Extensions
{
    /// <summary>
    /// 参数扩展
    /// </summary>
    public static class ParameterExtension
    {
        /// <summary>
        /// 类型化参数
        /// </summary>
        /// <param name="parameterType">参数类型</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns>类型化参数值</returns>
        public static object TypifyParameterValue(Type parameterType, string parameterValue)
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
                    return TypifyParameterValue(originalType, parameterValue);
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型外，都按对象反序列化
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };
                    typicalValue = JsonSerializer.Deserialize(parameterValue, parameterType, options);
                }

                return typicalValue;
            }

            return null;
        }

        /// <summary>
        /// 类型化参数
        /// </summary>
        /// <param name="parameterType">参数类型</param>
        /// <param name="parameterValue">参数值</param>
        /// <returns>类型化参数值</returns>
        public static object TypifyParameterValue(Type parameterType, object parameterValue)
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
                    typicalValue = Guid.Parse(parameterValue.ToString());
                }
                else if (parameterType == typeof(DateTime))
                {
                    typicalValue = DateTime.Parse(parameterValue.ToString());
                }
                else if (parameterType.IsEnum)
                {
                    typicalValue = Enum.Parse(parameterType, parameterValue.ToString());
                }
                else if (parameterType.IsPrimitive)
                {
                    typicalValue = Convert.ChangeType(parameterValue, parameterType);
                }
                else if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Type originalType = parameterType.GetGenericArguments()[0];
                    return TypifyParameterValue(originalType, parameterValue);
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型外，都按对象反序列化
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        IncludeFields = true
                    };
                    typicalValue = JsonSerializer.Deserialize(parameterValue.ToString(), parameterType, options);
                }

                return typicalValue;
            }

            return null;
        }
    }
}
