using System;

namespace SD.Toolkits.WebApi.Attributes
{
    /// <summary>
    /// 包装POST请求参数绑定特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class WrapPostParametersAttribute : Attribute
    {

    }
}