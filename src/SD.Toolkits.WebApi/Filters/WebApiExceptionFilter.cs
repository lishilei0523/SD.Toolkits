using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SD.Toolkits.WebApi.Filters
{
    /// <summary>
    /// WebApi异常过滤器
    /// </summary>
    public class WebApiExceptionFilter : IExceptionFilter
    {
        //Implements

        #region # 是否允许多实例 —— bool AllowMultiple
        /// <summary>
        /// 是否允许多实例
        /// </summary>
        public bool AllowMultiple
        {
            get { return false; }
        }
        #endregion

        #region # 执行异常事件过滤器 —— Task ExecuteExceptionFilterAsync(HttpActionExecutedContext....
        /// <summary>
        /// 执行异常事件过滤器
        /// </summary>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(context.Exception.Message, ref errorMessage);

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(errorMessage)
            };

            context.Response = httpResponseMessage;

            return Task.CompletedTask;
        }
        #endregion


        //Private

        #region # 递归获取错误消息 —— static string GetErrorMessage(string exceptionMessage...
        /// <summary>
        /// 递归获取错误消息
        /// </summary>
        /// <param name="exceptionMessage">异常消息</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>错误消息</returns>
        private static string GetErrorMessage(string exceptionMessage, ref string errorMessage)
        {
            try
            {
                const string errorMessageKey = "ErrorMessage";
                JObject jObject = (JObject)JsonConvert.DeserializeObject(exceptionMessage);
                if (jObject != null && jObject.ContainsKey(errorMessageKey))
                {
                    errorMessage = jObject.GetValue(errorMessageKey)?.ToString();
                }
                else
                {
                    errorMessage = exceptionMessage;
                }

                GetErrorMessage(errorMessage, ref errorMessage);

                return errorMessage;
            }
            catch
            {

                return exceptionMessage;
            }
        }
        #endregion
    }
}