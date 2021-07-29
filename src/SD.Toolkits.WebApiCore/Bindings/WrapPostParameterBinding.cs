using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SD.Toolkits.WebApiCore.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SD.Toolkits.WebApiCore.Bindings
{
    /// <summary>
    /// 包装POST请求参数模型绑定
    /// </summary>
    public class WrapPostParameterBinding : IModelBinder
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
            if (httpContext.Request.Method != HttpMethod.Post.ToString())
            {
                throw new InvalidOperationException("Only post method available !");
            }
            if (!httpContext.Request.ContentType.StartsWith("application/json") &&
                !httpContext.Request.ContentType.StartsWith("application/x-www-form-urlencoded"))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            #endregion

            NameValueCollection parameters = await this.ParseParametersFromBody(httpContext.Request);
            string parameterValue = parameters.Get(bindingContext.FieldName);
            object typicalValue = ParameterExtension.TypifyParameterValue(bindingContext.ModelType, parameterValue);
            bindingContext.Result = ModelBindingResult.Success(typicalValue);
        }
        #endregion


        //Private

        #region # 读取转换参数至字典 —— async Task<NameValueCollection> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private async Task<NameValueCollection> ParseParametersFromBody(HttpRequest request)
        {
            string cacheKey = typeof(WrapPostParameterBinding).FullName;
            if (!request.HttpContext.Items.TryGetValue(cacheKey, out object result))
            {
                switch (request.ContentType)
                {
                    case "application/json":
                        using (StreamReader streamReader = new StreamReader(request.Body, Encoding.UTF8, false, 4096, true))
                        {
                            string body = await streamReader.ReadToEndAsync();
                            IDictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                            result = values.Aggregate(new NameValueCollection(), (seed, current) =>
                            {
                                seed.Add(current.Key, current.Value?.ToString());
                                return seed;
                            });
                        }
                        break;
                    case "application/x-www-form-urlencoded":
                        NameValueCollection collection = new NameValueCollection();
                        foreach (KeyValuePair<string, StringValues> kv in request.Form)
                        {
                            collection.Add(kv.Key, kv.Value.ToString());
                        }
                        break;
                    default:
                        throw new HttpRequestException(HttpStatusCode.UnsupportedMediaType.ToString());
                }

                request.HttpContext.Items.Add(cacheKey, result);
            }

            return (NameValueCollection)result;
        }
        #endregion
    }
}
