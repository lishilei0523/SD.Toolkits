using Microsoft.Owin;
using Owin;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Filters;
using SD.Toolkits.WebApi.Tests;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.Toolkits.WebApi.Tests
{
    public class Startup
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            //注册参数绑定
            httpConfiguration.RegisterComplexGetParameterBindingRule();
            httpConfiguration.RegisterWrapParameterBindingRule();
            httpConfiguration.RegisterFileParameterBindingRule();

            //异常过滤器
            httpConfiguration.Filters.Add(new WebApiExceptionFilter());

            //配置跨域
            httpConfiguration.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //配置WebApi
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}
