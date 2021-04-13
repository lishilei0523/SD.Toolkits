using System;
using System.Reflection;

namespace SD.Common
{
    /// <summary>
    /// object扩展方法
    /// </summary>
    public static class ObjectExtension
    {
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
