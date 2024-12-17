using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SD.Toolkits.AspNetCore.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SD.Toolkits.AspNetCore.Bindings
{
    /// <summary>
    /// 文件参数模型绑定
    /// </summary>
    public class FileParameterBinding : IModelBinder
    {
        #region # 字段及构造器

        /// <summary>
        /// JSON序列化设置
        /// </summary>
        private readonly JsonSerializerOptions _jsonSerializerSettings;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public FileParameterBinding(IOptions<JsonOptions> jsonOptions)
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
            if (httpContext.Request.Method != HttpMethod.Post.ToString())
            {
                throw new InvalidOperationException("Only post method available !");
            }
            if (!httpContext.Request.ContentType.StartsWith("multipart/form-data"))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            #endregion

            IDictionary<string, object> parameters = this.ParseParametersFromBody(httpContext.Request);
            object parameterValue = parameters!.ContainsKey(bindingContext.FieldName)
                ? parameters[bindingContext.FieldName]
                : null;
            if (parameterValue != null)
            {
                object typicalValue;
                if (bindingContext.ModelType == typeof(IFormFile))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(parameterValue.ToString()) || parameterValue.ToString() == undefined)
                    {
                        typicalValue = null;
                    }
                    else
                    {
                        if (parameterValue is IFormFileCollection formFiles)
                        {
                            typicalValue = formFiles[0];
                        }
                        else if (parameterValue is IFormFile formFile)
                        {
                            typicalValue = formFile;
                        }
                        else
                        {
                            typicalValue = null;
                        }
                    }
                }
                else if (bindingContext.ModelType == typeof(IFormFileCollection))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(parameterValue.ToString()) || parameterValue.ToString() == undefined)
                    {
                        typicalValue = new FormFileCollection();
                    }
                    else
                    {
                        if (parameterValue is IFormFileCollection formFiles)
                        {
                            typicalValue = formFiles;
                        }
                        else if (parameterValue is IFormFile formFile)
                        {
                            FormFileCollection formFileCollection = new FormFileCollection
                            {
                                formFile
                            };
                            typicalValue = formFileCollection;
                        }
                        else
                        {
                            typicalValue = new FormFileCollection();
                        }
                    }
                }
                else
                {
                    //除文件外，按类型化参数处理
                    typicalValue = ParameterExtension.TypifyParameterValue(bindingContext.ModelType, parameterValue, this._jsonSerializerSettings);
                }

                bindingContext.Result = ModelBindingResult.Success(typicalValue);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }

            return Task.CompletedTask;
        }
        #endregion


        //Private

        #region # 读取转换参数至字典 —— IDictionary<string, object> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private IDictionary<string, object> ParseParametersFromBody(HttpRequest request)
        {
            string cacheKey = typeof(FileParameterBinding).FullName;
            if (!request.HttpContext.Items.TryGetValue(cacheKey!, out object result))
            {
                IDictionary<string, object> formDatas = new ConcurrentDictionary<string, object>();
                foreach (IFormFile formFile in request.Form.Files)
                {
                    if (formDatas.ContainsKey(formFile.Name))
                    {
                        object formData = formDatas[formFile.Name];
                        if (formData is IFormFile prevFile)
                        {
                            FormFileCollection formFiles = new FormFileCollection
                            {
                                prevFile,
                                formFile
                            };
                            formDatas[formFile.Name] = formFiles;
                        }
                        if (formData is FormFileCollection existedFiles)
                        {
                            existedFiles.Add(formFile);
                        }
                    }
                    else
                    {
                        formDatas.Add(formFile.Name, formFile);
                    }
                }
                foreach (KeyValuePair<string, StringValues> kv in request.Form)
                {
                    formDatas.Add(kv.Key, kv.Value.ToString());
                }

                result = formDatas;
                request.HttpContext.Items.Add(cacheKey, result);
            }

            return (IDictionary<string, object>)result;
        }
        #endregion
    }
}
