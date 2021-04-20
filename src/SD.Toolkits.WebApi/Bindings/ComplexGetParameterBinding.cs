using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public ComplexGetParameterBinding(HttpParameterDescriptor parameterDescriptor)
            : base(parameterDescriptor)
        {

        }

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