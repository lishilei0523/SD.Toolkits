using System;
using System.Reflection;

namespace SD.Common
{
    /// <summary>
    /// object扩展
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
            //获取源对象与目标对象的类型
            Type sourceType = sourceInstance.GetType();
            Type targetType = targetInstance.GetType();

            //获取源对象与目标对象的所有属性
            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            PropertyInfo[] targetProperties = targetType.GetProperties();

            //双重遍历，判断属性名称是否相同，如果相同则赋值
            foreach (PropertyInfo targetProperty in targetProperties)
            {
                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    if (targetProperty.Name == sourceProperty.Name)
                    {
                        targetProperty.SetValue(targetInstance, sourceProperty.GetValue(sourceInstance, null), null);
                    }
                }
            }
        }
        #endregion

        #region # 设置应用程序域属性分配值 —— static void SetData<T>(this AppDomain...
        /// <summary>
        /// 为应用程序域属性分配指定值
        /// </summary>
        /// <param name="appDomain">应用程序域</param>
        /// <param name="name">要创建或更改的用户定义应用程序域属性的名称</param>
        /// <param name="data">属性的值</param>
        public static void SetData<T>(this AppDomain appDomain, string name, T data)
        {
            appDomain.SetData(name, data);
        }
        #endregion

        #region # 获取应用程序域属性分配值 —— static T GetData<T>(this AppDomain...
        /// <summary>
        /// 获取应用程序域属性分配值
        /// </summary>
        /// <param name="appDomain">应用程序域</param>
        /// <param name="name">要创建或更改的用户定义应用程序域属性的名称</param>
        /// <returns>属性的值</returns>
        public static T GetData<T>(this AppDomain appDomain, string name)
        {
            object data = appDomain.GetData(name);
            if (data == null)
            {
                return default(T);
            }
            if (data is T value)
            {
                return value;
            }

            return default(T);
        }
        #endregion
    }
}
