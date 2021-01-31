using System;

namespace SD.Toolkits.WebApi.Extensions
{
    /// <summary>
    /// Use this attribute on API methods that need to support multiple POST parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class WrapPostParametersAttribute : Attribute
    {

    }
}