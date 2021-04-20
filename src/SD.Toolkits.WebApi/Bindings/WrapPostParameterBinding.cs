using Newtonsoft.Json;
using SD.Toolkits.WebApi.Attributes;
using System;
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
            string stringValue = parameters.Get(base.Descriptor.ParameterName);
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                object paramValue;
                if (base.Descriptor.ParameterType == typeof(string))
                {
                    paramValue = stringValue;
                }
                else if (base.Descriptor.ParameterType == typeof(Guid))
                {
                    paramValue = Guid.Parse(stringValue);
                }
                else if (base.Descriptor.ParameterType == typeof(DateTime))
                {
                    paramValue = DateTime.Parse(stringValue);
                }
                else if (base.Descriptor.ParameterType.IsEnum)
                {
                    paramValue = Enum.Parse(base.Descriptor.ParameterType, stringValue);
                }
                else if (base.Descriptor.ParameterType.IsPrimitive)
                {
                    paramValue = Convert.ChangeType(stringValue, base.Descriptor.ParameterType);
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型外，都按对象反序列化
                    paramValue = JsonConvert.DeserializeObject(stringValue, base.Descriptor.ParameterType);
                }

                base.SetValue(actionContext, paramValue);
            }
            else
            {
                base.SetValue(actionContext, null);
            }
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
                switch (contentType.MediaType)
                {
                    case "application/json":
                        string content = await request.Content.ReadAsStringAsync();
                        IDictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                        result = values.Aggregate(new NameValueCollection(), (seed, current) =>
                        {
                            seed.Add(current.Key, current.Value?.ToString());
                            return seed;
                        });
                        break;
                    case "application/x-www-form-urlencoded":
                        result = await request.Content.ReadAsFormDataAsync();
                        break;
                    default:
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