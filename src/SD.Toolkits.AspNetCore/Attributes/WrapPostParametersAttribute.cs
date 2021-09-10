using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SD.Toolkits.AspNetCore.Bindings;
using System;

namespace SD.Toolkits.AspNetCore.Attributes
{
    /// <summary>
    /// 包装POST请求参数绑定特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class WrapPostParametersAttribute : Attribute, IActionModelConvention
    {
        /// <summary>
        /// 适用
        /// </summary>
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