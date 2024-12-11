using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SD.Toolkits.AspNetCore.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SD.Toolkits.AspNetCore.Bindings
{
    /// <summary>
    /// 包装POST请求参数模型绑定
    /// </summary>
    public class WrapPostParameterBinding : IModelBinder
    {
        #region # 字段及构造器

        /// <summary>
        /// JSON序列化设置
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerSettings;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public WrapPostParameterBinding(IOptions<JsonOptions> jsonOptions)
        {
            this._jsonSerializerSettings = jsonOptions.Value.JsonSerializerOptions;
        }

        #endregion


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
            if (!httpContext.Request.ContentType!.StartsWith("application/json") &&
                !httpContext.Request.ContentType!.StartsWith("application/x-www-form-urlencoded"))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            #endregion

            NameValueCollection parameters = await this.ParseParametersFromBody(httpContext.Request);
            string parameterValue = parameters.Get(bindingContext.FieldName);
            object typicalValue = ParameterExtension.TypifyParameterValue(bindingContext.ModelType, parameterValue, this._jsonSerializerSettings);
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
            if (!request.HttpContext.Items.TryGetValue(cacheKey!, out object result))
            {
                if (request.ContentType!.StartsWith("application/json"))
                {
                    using StreamReader streamReader = new StreamReader(request.Body, Encoding.UTF8, false, 4096, true);
                    string body = await streamReader.ReadToEndAsync();
                    IDictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(body, this._jsonSerializerSettings);
                    result = values.Aggregate(new NameValueCollection(), (seed, current) =>
                    {
                        seed.Add(current.Key, current.Value?.ToString());
                        return seed;
                    });
                }
                else if (request.ContentType!.StartsWith("application/x-www-form-urlencoded"))
                {
                    NameValueCollection collection = new NameValueCollection();
                    foreach (KeyValuePair<string, StringValues> kv in request.Form)
                    {
                        collection.Add(kv.Key, kv.Value.ToString());
                    }
                    result = collection;
                }
                else
                {
                    throw new HttpRequestException(HttpStatusCode.UnsupportedMediaType.ToString());
                }

                request.HttpContext.Items.Add(cacheKey, result);
            }

            return (NameValueCollection)result;
        }
        #endregion
    }
}
