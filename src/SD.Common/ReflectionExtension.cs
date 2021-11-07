using System;
using System.Reflection;
using System.Text;

namespace SD.Common
{
    /// <summary>
    /// 反射扩展
    /// </summary>
    public static class ReflectionExtension
    {
        #region # 获取方法路径 —— string GetMethodPath(this MethodBase method)
        /// <summary>
        /// 获取方法路径
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法路径</returns>
        public static string GetMethodPath(this MethodBase method)
        {
            #region # 验证

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), @"方法信息不可为空！");
            }

            #endregion

            const string separator = "/";
            string assemblyName = method.DeclaringType?.Assembly.GetName().Name;
            string @namespace = method.DeclaringType?.Namespace;
            string className = method.DeclaringType?.Name;

            StringBuilder pathBuilder = new StringBuilder(separator);
            pathBuilder.Append(assemblyName);
            pathBuilder.Append(separator);
            pathBuilder.Append(@namespace);
            pathBuilder.Append(separator);
            pathBuilder.Append(className);
            pathBuilder.Append(separator);
            pathBuilder.Append(method.Name);

            return pathBuilder.ToString();
        }
        #endregion

        #region # 判断给定类型是否是Nullable —— static bool IsNullable(this Type type)
        /// <summary>
        /// 判断给定类型是否是Nullable
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是Nullable</returns>
        public static bool IsNullable(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
