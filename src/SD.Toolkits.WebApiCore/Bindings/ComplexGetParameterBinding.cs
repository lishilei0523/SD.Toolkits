using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SD.Toolkits.WebApiCore.Bindings
{
    /// <summary>
    /// GET请求复杂参数绑定
    /// </summary>
    public class ComplexGetParameterBinding : IModelBinder
    {
        //Implements

        #region # 执行参数模型绑定 —— async Task BindModelAsync(ModelBindingContext bindingContext)
        /// <summary>
        /// 执行参数模型绑定
        /// </summary>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpContext httpContext = bindingContext.HttpContext;

            #region # 验证

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (httpContext.Request.Method != HttpMethod.Get.ToString())
            {
                throw new InvalidOperationException("Only get method available !");
            }

            #endregion

            NameValueCollection parameters = await this.ParseParametersFromBody(httpContext.Request);
            string parameterValue = parameters.Get(bindingContext.FieldName);
            parameterValue = WebUtility.UrlDecode(parameterValue);
            if (!string.IsNullOrWhiteSpace(parameterValue))
            {
                object paramValue = JsonConvert.DeserializeObject(parameterValue, bindingContext.ModelType);
                bindingContext.Result = ModelBindingResult.Success(paramValue);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }
        }
        #endregion


        //Private

        #region # 读取转换参数至字典 —— async Task<NameValueCollection> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private async Task<NameValueCollection> ParseParametersFromBody(HttpRequest request)
        {
            string cacheKey = typeof(ComplexGetParameterBinding).FullName;
            if (!request.HttpContext.Items.TryGetValue(cacheKey, out object result))
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                foreach (KeyValuePair<string, StringValues> keyValuePair in request.Query)
                {
                    nameValueCollection.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                }

                result = nameValueCollection;
                request.HttpContext.Items.Add(cacheKey, result);
            }

            return (NameValueCollection)result;
        }
        #endregion
    }
}
