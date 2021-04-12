using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SD.Toolkits.WebApiCore.Extensions
{
    /// <summary>
    /// 文件参数绑定特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class FileParametersAttribute : Attribute, IActionModelConvention
    {
        /// <summary>
        /// 适用
        /// </summary>
        public void Apply(ActionModel action)
        {
            foreach (ParameterModel parameter in action.Parameters)
            {
                parameter.BindingInfo ??= new BindingInfo();
                parameter.BindingInfo.BinderType = typeof(FileParameterBinding);
            }
        }
    }
}