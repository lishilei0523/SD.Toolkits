using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SD.Toolkits.WebApiCore.Extensions
{
    /// <summary>
    /// Use this attribute on API methods that need to support multiple POST parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class WrapPostParametersAttribute : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            foreach (ParameterModel parameter in action.Parameters)
            {
                parameter.BindingInfo ??= new BindingInfo();
                parameter.BindingInfo.BinderType = typeof(WrapPostParameterBinding);
            }
        }
    }
}