using System;

namespace SD.Toolkits.WebApi.Attributes
{
    /// <summary>
    /// GET请求复杂参数绑定特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class ComplexGetParametersAttribute : Attribute
    {

    }
}