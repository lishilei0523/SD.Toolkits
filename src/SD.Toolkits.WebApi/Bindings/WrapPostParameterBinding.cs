using Newtonsoft.Json;
using SD.Toolkits.WebApi.Attributes;
using SD.Toolkits.WebApi.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace SD.Toolkits.WebApi.Bindings
{
    /// <summary>
    /// 包装POST请求参数绑定
    /// </summary>
    public sealed class WrapPostParameterBinding : HttpParameterBinding
    {
        #region # 构造器

        #region 00.私有构造器
        /// <summary>
        /// 私有构造器
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        private WrapPostParameterBinding(HttpParameterDescriptor parameterDescriptor)
            : base(parameterDescriptor)
        {

        }
        #endregion

        #region 01.创建包装POST请求参数绑定 —— static WrapPostParameterBinding CreateBindingForMarkedAction(...
        /// <summary>
        /// 创建包装POST请求参数绑定
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public static WrapPostParameterBinding CreateBindingForMarkedAction(HttpParameterDescriptor parameterDescriptor)
        {
            if (!parameterDescriptor.ActionDescriptor.GetCustomAttributes<WrapPostParametersAttribute>().Any())
            {
                return null;
            }
            if (parameterDescriptor.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Post))
            {
                return new WrapPostParameterBinding(parameterDescriptor);
            }

            return null;
        }
        #endregion

        #endregion

        #region # 方法

        #region # 执行参数绑定 —— override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider...
        /// <summary>
        /// 执行参数绑定
        /// </summary>
        public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            NameValueCollection parameters = await this.ParseParametersFromBody(actionContext.Request);
            string parameterValue = parameters.Get(base.Descriptor.ParameterName);
            object typicalValue = ParameterExtension.TypifyParameterValue(base.Descriptor.ParameterType, parameterValue);

            base.SetValue(actionContext, typicalValue);
        }
        #endregion

        #region # 读取转换参数至字典 —— async Task<NameValueCollection> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private async Task<NameValueCollection> ParseParametersFromBody(HttpRequestMessage request)
        {
            string cacheKey = typeof(WrapPostParameterBinding).FullName;
            if (!request.Properties.TryGetValue(cacheKey, out object result))
            {
                MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
                if (contentType.MediaType.StartsWith("application/json"))
                {
                    string content = await request.Content.ReadAsStringAsync();
                    IDictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                    result = values.Aggregate(new NameValueCollection(), (seed, current) =>
                    {
                        seed.Add(current.Key, current.Value?.ToString());
                        return seed;
                    });
                }
                else if (contentType.MediaType.StartsWith("application/x-www-form-urlencoded"))
                {
                    result = await request.Content.ReadAsFormDataAsync();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                request.Properties.Add(cacheKey, result);
            }

            return (NameValueCollection)result;
        }
        #endregion

        #endregion
    }
}