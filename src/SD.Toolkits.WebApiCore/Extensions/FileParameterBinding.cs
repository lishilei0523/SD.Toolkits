using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SD.Toolkits.WebApiCore.Extensions
{
    /// <summary>
    /// 文件参数模型模型绑定
    /// </summary>
    public class FileParameterBinding : IModelBinder
    {
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
            object objectValue = parameters.ContainsKey(bindingContext.FieldName)
                ? parameters[bindingContext.FieldName]
                : null;
            if (objectValue != null)
            {
                object paramValue;
                if (bindingContext.ModelType == typeof(string))
                {
                    paramValue = objectValue.ToString();
                }
                else if (bindingContext.ModelType == typeof(Guid))
                {
                    paramValue = Guid.Parse(objectValue.ToString());
                }
                else if (bindingContext.ModelType == typeof(DateTime))
                {
                    paramValue = DateTime.Parse(objectValue.ToString());
                }
                else if (bindingContext.ModelType.IsEnum)
                {
                    paramValue = Enum.Parse(bindingContext.ModelType, objectValue.ToString());
                }
                else if (bindingContext.ModelType.IsPrimitive)
                {
                    paramValue = Convert.ChangeType(objectValue.ToString(), bindingContext.ModelType);
                }
                else if (bindingContext.ModelType == typeof(IFormFile))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(objectValue.ToString()) || objectValue.ToString() == undefined)
                    {
                        paramValue = null;
                    }
                    else
                    {
                        if (objectValue is IFormFileCollection formFiles)
                        {
                            paramValue = formFiles[0];
                        }
                        else if (objectValue is IFormFile formFile)
                        {
                            paramValue = formFile;
                        }
                        else
                        {
                            paramValue = null;
                        }
                    }
                }
                else if (bindingContext.ModelType == typeof(IFormFileCollection))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(objectValue.ToString()) || objectValue.ToString() == undefined)
                    {
                        paramValue = null;
                    }
                    else
                    {
                        if (objectValue is IFormFileCollection formFiles)
                        {
                            paramValue = formFiles;
                        }
                        else if (objectValue is IFormFile formFile)
                        {
                            FormFileCollection formFileCollection = new FormFileCollection();
                            formFileCollection.Add(formFile);

                            paramValue = formFileCollection;
                        }
                        else
                        {
                            paramValue = null;
                        }
                    }
                }
                else
                {
                    //除字符串、Guid、时间、枚举、基元类型、文件外，都按对象反序列化
                    paramValue = JsonSerializer.Deserialize(objectValue.ToString(), bindingContext.ModelType);
                }

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

        #region # 读取转换参数至字典 —— IDictionary<string, object> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private IDictionary<string, object> ParseParametersFromBody(HttpRequest request)
        {
            string cacheKey = typeof(FileParameterBinding).FullName;
            if (!request.HttpContext.Items.TryGetValue(cacheKey, out object result))
            {
                IDictionary<string, object> formDatas = new ConcurrentDictionary<string, object>();
                foreach (IFormFile formFile in request.Form.Files)
                {
                    if (formDatas.ContainsKey(formFile.Name))
                    {
                        object formData = formDatas[formFile.Name];
                        if (formData is IFormFile prevFile)
                        {
                            FormFileCollection formFiles = new FormFileCollection();
                            formFiles.Add(prevFile);
                            formFiles.Add(formFile);
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
