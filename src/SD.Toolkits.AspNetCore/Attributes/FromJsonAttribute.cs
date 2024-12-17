using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SD.Toolkits.AspNetCore.Bindings;
using System;

namespace SD.Toolkits.AspNetCore.Attributes
{
    /// <summary>
    /// 复杂参数JSON绑定特性
    /// </summary>
    /// <remarks>只适用于GET请求的参数</remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FromJsonAttribute : Attribute, IParameterModelConvention
    {
        /// <summary>
        /// 适用
        /// </summary>
        public void Apply(ParameterModel parameter)
        {
            parameter.BindingInfo ??= new BindingInfo();
            parameter.BindingInfo.BinderType = typeof(ComplexGetParameterBinding);
        }
    }
}
