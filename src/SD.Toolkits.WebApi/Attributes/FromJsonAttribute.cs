using System;

namespace SD.Toolkits.WebApi.Attributes
{
    /// <summary>
    /// JSON序列化参数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FromJsonAttribute : Attribute
    {

    }
}