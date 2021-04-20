using Newtonsoft.Json;
using SD.Toolkits.WebApi.Attributes;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace SD.Toolkits.WebApi.Bindings
{
    /// <summary>
    /// GET请求复杂参数绑定
    /// </summary>
    public sealed class ComplexGetParameterBinding : HttpParameterBinding
    {
        #region # 构造器

        #region 00.私有构造器
        /// <summary>
        /// 私有构造器
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        private ComplexGetParameterBinding(HttpParameterDescriptor parameterDescriptor)
            : base(parameterDescriptor)
        {

        }
        #endregion

        #region 01.创建GET请求复杂参数绑定 —— static ComplexGetParameterBinding CreateBindingForMarkedAction(...
        /// <summary>
        /// 创建GET请求复杂参数绑定
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public static ComplexGetParameterBinding CreateBindingForMarkedAction(HttpParameterDescriptor parameterDescriptor)
        {
            if (!parameterDescriptor.ActionDescriptor.GetCustomAttributes<ComplexGetParametersAttribute>().Any())
            {
                return null;
            }
            if (parameterDescriptor.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get))
            {
                return new ComplexGetParameterBinding(parameterDescriptor);
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
            //JSON复杂参数
            if (base.Descriptor.GetCustomAttributes<FromJsonAttribute>().Any())
            {
                NameValueCollection parameters = await this.ParseParametersFromBody(actionContext.Request);
                string stringValue = parameters.Get(base.Descriptor.ParameterName);
                stringValue = WebUtility.UrlDecode(stringValue);

                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    object paramValue = JsonConvert.DeserializeObject(stringValue, base.Descriptor.ParameterType);
                    base.SetValue(actionContext, paramValue);
                }
                else
                {
                    base.SetValue(actionContext, null);
                }
            }
        }
        #endregion

        #region # 读取转换参数至字典 —— async Task<NameValueCollection> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private async Task<NameValueCollection> ParseParametersFromBody(HttpRequestMessage request)
        {
            string cacheKey = typeof(ComplexGetParameterBinding).FullName;
            if (!request.Properties.TryGetValue(cacheKey, out object result))
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                IEnumerable<KeyValuePair<string, string>> queryStringPairs = request.GetQueryNameValuePairs();
                foreach (KeyValuePair<string, string> keyValuePair in queryStringPairs)
                {
                    nameValueCollection.Add(keyValuePair.Key, keyValuePair.Value);
                }

                result = nameValueCollection;
                request.Properties.Add(cacheKey, result);
            }

            return (NameValueCollection)result;
        }
        #endregion

        #endregion
    }
}