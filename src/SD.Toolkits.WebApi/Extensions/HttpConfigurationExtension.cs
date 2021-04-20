using SD.Toolkits.WebApi.Bindings;
using System.Web.Http;

namespace SD.Toolkits.WebApi.Extensions
{
    /// <summary>
    /// Http配置扩展
    /// </summary>
    public static class HttpConfigurationExtension
    {
        /// <summary>
        /// 注册GET请求复杂参数绑定
        /// </summary>
        /// <param name="httpConfiguration">Http配置</param>
        public static void RegisterComplexGetParameterBindingRule(this HttpConfiguration httpConfiguration)
        {
            httpConfiguration.ParameterBindingRules.Insert(0, ComplexGetParameterBinding.CreateBindingForMarkedAction);
        }

        /// <summary>
        /// 注册POST请求多参数绑定
        /// </summary>
        /// <param name="httpConfiguration">Http配置</param>
        public static void RegisterWrapParameterBindingRule(this HttpConfiguration httpConfiguration)
        {
            httpConfiguration.ParameterBindingRules.Insert(0, WrapPostParameterBinding.CreateBindingForMarkedAction);
        }

        /// <summary>
        /// 注册文件参数绑定
        /// </summary>
        /// <param name="httpConfiguration">Http配置</param>
        public static void RegisterFileParameterBindingRule(this HttpConfiguration httpConfiguration)
        {
            httpConfiguration.ParameterBindingRules.Insert(0, FileParameterBinding.CreateBindingForMarkedAction);
        }
    }
}
