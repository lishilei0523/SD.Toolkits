using SD.Toolkits.WebApi.Bindings;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SD.Toolkits.WebApi.Attributes
{
    /// <summary>
    /// 复杂参数JSON反序列化绑定特性
    /// </summary>
    /// <remarks>只适用于GET请求的参数</remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class FromJsonAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// 获取参数绑定
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameterDescriptor)
        {
            if (parameterDescriptor.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get))
            {
                return new ComplexGetParameterBinding(parameterDescriptor);
            }

            return null;
        }
    }
}