using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Toolkits.AspNet;
using System;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SD.Toolkits.AspNetMvcCore.Filters
{
    /// <summary>
    /// ASP.NET Core MVC异常过滤器
    /// </summary>
    public class MvcExceptionFilter : IExceptionFilter
    {
        //Implements

        #region # 执行异常过滤器事件 —— void OnException(ExceptionContext context)
        /// <summary>
        /// 执行异常过滤器事件
        /// </summary>
        public async void OnException(ExceptionContext context)
        {
            Exception innerException = GetInnerException(context.Exception);

            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(innerException.Message, ref errorMessage);

            //设置状态码为500
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //Ajax请求
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                //响应
                await context.HttpContext.Response.WriteAsync(errorMessage);
            }
            else
            {
                //构造脚本
                StringBuilder scriptBuilder = new StringBuilder();
                scriptBuilder.Append("<script type=\"text/javascript\">");
                scriptBuilder.Append("window.top.location.href=");
                scriptBuilder.AppendFormat("\"{0}?message={1}\"", AspNetSection.Setting.ErrorPage.Value, errorMessage);
                scriptBuilder.Append("</script>");

                //跳转至错误页
                await context.HttpContext.Response.WriteAsync(scriptBuilder.ToString());
            }

            //异常已处理
            context.ExceptionHandled = true;
        }
        #endregion


        //Private

        #region # 递归获取内部异常 —— static Exception GetInnerException(Exception exception)
        /// <summary>
        /// 递归获取内部异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>内部异常</returns>
        private static Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnerException(exception.InnerException);
            }

            return exception;
        }
        #endregion

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
                JsonDocument jsonDocument = JsonDocument.Parse(exceptionMessage);
                if (jsonDocument.RootElement.TryGetProperty(errorMessageKey, out JsonElement messageElement))
                {
                    errorMessage = messageElement.ToString();
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

        #region # 判断是否是Ajax请求 —— static bool IsAjaxRequest(HttpRequest request)
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <returns>是否是Ajax请求</returns>
        private static bool IsAjaxRequest(HttpRequest request)
        {
            #region # 验证

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            #endregion

            const string ajaxHeaderKey = "X-Requested-With";
            const string ajaxHeaderValue = "XMLHttpRequest";

            bool isAjax = false;
            bool hasAjaxHeader = request.Headers.ContainsKey(ajaxHeaderKey);
            if (hasAjaxHeader)
            {
                isAjax = string.Equals(request.Headers[ajaxHeaderKey], ajaxHeaderValue, StringComparison.OrdinalIgnoreCase);
            }

            return isAjax;
        }
        #endregion
    }
}