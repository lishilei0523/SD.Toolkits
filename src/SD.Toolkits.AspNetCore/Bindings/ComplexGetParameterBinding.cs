﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SD.Toolkits.AspNetCore.Bindings
{
    /// <summary>
    /// GET请求复杂参数模型绑定
    /// </summary>
    public class ComplexGetParameterBinding : IModelBinder
    {
        #region # 字段及构造器

        /// <summary>
        /// JSON序列化设置
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerSettings;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ComplexGetParameterBinding(IOptions<JsonOptions> jsonOptions)
        {
            this._jsonSerializerSettings = jsonOptions.Value.JsonSerializerOptions;
        }

        #endregion


        //Implements

        #region # 执行参数模型绑定 —— Task BindModelAsync(ModelBindingContext bindingContext)
        /// <summary>
        /// 执行参数模型绑定
        /// </summary>
        public Task BindModelAsync(ModelBindingContext bindingContext)
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

            NameValueCollection parameters = this.ParseParametersFromBody(httpContext.Request);
            string parameterValue = parameters.Get(bindingContext.FieldName);
            parameterValue = WebUtility.UrlDecode(parameterValue);
            if (!string.IsNullOrWhiteSpace(parameterValue))
            {
                object paramValue = JsonSerializer.Deserialize(parameterValue, bindingContext.ModelType, this._jsonSerializerSettings);
                bindingContext.Result = ModelBindingResult.Success(paramValue);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }

            return Task.CompletedTask;
        }
        #endregion


        //Private

        #region # 读取转换参数至字典 —— NameValueCollection ParseParametersFromBody(HttpRequest request)
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private NameValueCollection ParseParametersFromBody(HttpRequest request)
        {
            string cacheKey = typeof(ComplexGetParameterBinding).FullName;
            if (!request.HttpContext.Items.TryGetValue(cacheKey!, out object result))
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
