using Newtonsoft.Json;
using SD.Toolkits.WebApi.Attributes;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace SD.Toolkits.WebApi.Bindings
{
    /// <summary>
    /// 文件参数绑定
    /// </summary>
    public sealed class FileParameterBinding : HttpParameterBinding
    {
        #region # 构造器

        #region 00.私有构造器
        /// <summary>
        /// 私有构造器
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public FileParameterBinding(HttpParameterDescriptor parameterDescriptor)
            : base(parameterDescriptor)
        {

        }
        #endregion

        #region 01.创建文件参数绑定 —— static FileParameterBinding CreateBindingForMarkedAction(...
        /// <summary>
        /// 创建文件参数绑定
        /// </summary>
        /// <param name="parameterDescriptor">参数描述者</param>
        public static FileParameterBinding CreateBindingForMarkedAction(HttpParameterDescriptor parameterDescriptor)
        {
            if (!parameterDescriptor.ActionDescriptor.GetCustomAttributes<FileParametersAttribute>().Any())
            {
                return null;
            }
            if (parameterDescriptor.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Post))
            {
                return new FileParameterBinding(parameterDescriptor);
            }

            return null;
        }
        #endregion

        #endregion

        #region # 方法

        #region 执行参数绑定 —— override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider...
        /// <summary>
        /// 执行参数绑定
        /// </summary>
        public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            #region # 验证

            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            #endregion

            IDictionary<string, object> parameters = await this.ParseParametersFromBody(actionContext.Request);
            object parameterValue = parameters.ContainsKey(base.Descriptor.ParameterName)
                ? parameters[base.Descriptor.ParameterName]
                : null;

            if (parameterValue != null)
            {
                object paramValue;
                if (base.Descriptor.ParameterType == typeof(IFormFile))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(parameterValue.ToString()) || parameterValue.ToString() == undefined)
                    {
                        paramValue = null;
                    }
                    else
                    {
                        if (parameterValue is IFormFileCollection formFiles)
                        {
                            paramValue = formFiles[0];
                        }
                        else if (parameterValue is IFormFile formFile)
                        {
                            paramValue = formFile;
                        }
                        else
                        {
                            paramValue = null;
                        }
                    }
                }
                else if (base.Descriptor.ParameterType == typeof(IFormFileCollection))
                {
                    const string undefined = "undefined";
                    if (string.IsNullOrWhiteSpace(parameterValue.ToString()) || parameterValue.ToString() == undefined)
                    {
                        paramValue = new FormFileCollection();
                    }
                    else
                    {
                        if (parameterValue is IFormFileCollection formFiles)
                        {
                            paramValue = formFiles;
                        }
                        else if (parameterValue is IFormFile formFile)
                        {
                            FormFileCollection formFileCollection = new FormFileCollection();
                            formFileCollection.Add(formFile);

                            paramValue = formFileCollection;
                        }
                        else
                        {
                            paramValue = new FormFileCollection();
                        }
                    }
                }
                else
                {
                    //除文件外，按类型化参数处理
                    JsonSerializerSettings jsonSerializerSettings = actionContext.ControllerContext.Configuration.Formatters.JsonFormatter.SerializerSettings;
                    paramValue = ParameterExtension.TypifyParameterValue(base.Descriptor.ParameterType, parameterValue, jsonSerializerSettings);
                }

                base.SetValue(actionContext, paramValue);
            }
            else
            {
                base.SetValue(actionContext, null);
            }
        }
        #endregion

        #region 读取转换参数至字典 —— async Task<IDictionary<string, object>> ParseParametersFromBody(...
        /// <summary>
        /// 读取转换参数至字典
        /// </summary>
        private async Task<IDictionary<string, object>> ParseParametersFromBody(HttpRequestMessage request)
        {
            string cacheKey = typeof(FileParameterBinding).FullName;
            if (!request.Properties.TryGetValue(cacheKey, out object result))
            {
                MultipartMemoryStreamProvider streamProvider = new MultipartMemoryStreamProvider();
                streamProvider = request.Content.ReadAsMultipartAsync(streamProvider).Result;
                IDictionary<string, object> formDatas = new ConcurrentDictionary<string, object>();
                foreach (HttpContent httpContent in streamProvider.Contents)
                {
                    string formName = httpContent.Headers.ContentDisposition.Name.Replace("\"", string.Empty);
                    string fileName = httpContent.Headers.ContentDisposition.FileName?.Replace("\"", string.Empty);

                    //文件
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        Stream fileStream = await httpContent.ReadAsStreamAsync();
                        using (BinaryReader binaryReader = new BinaryReader(fileStream))
                        {
                            byte[] fileBuffer = binaryReader.ReadBytes((int)fileStream.Length);
                            IFormFile currentFile = new FormFile
                            {
                                Name = formName,
                                FileName = fileName,
                                ContentType = httpContent.Headers.ContentType.MediaType,
                                ContentLength = httpContent.Headers.ContentLength ?? 0,
                                ContentDisposition = httpContent.Headers.ContentDisposition.ToString(),
                                Datas = fileBuffer
                            };

                            if (formDatas.ContainsKey(formName))
                            {
                                object formData = formDatas[formName];
                                if (formData is IFormFile prevFile)
                                {
                                    FormFileCollection formFiles = new FormFileCollection();
                                    formFiles.Add(prevFile);
                                    formFiles.Add(currentFile);

                                    formDatas[formName] = formFiles;
                                }
                                if (formData is FormFileCollection existedFiles)
                                {
                                    existedFiles.Add(currentFile);
                                }
                            }
                            else
                            {
                                formDatas.Add(formName, currentFile);
                            }
                        }
                    }
                    //表单
                    else
                    {
                        string formValue = await httpContent.ReadAsStringAsync();
                        formDatas.Add(formName, formValue);
                    }
                }

                result = formDatas;
                request.Properties.Add(cacheKey, result);
            }

            return (IDictionary<string, object>)result;
        }
        #endregion

        #endregion
    }
}