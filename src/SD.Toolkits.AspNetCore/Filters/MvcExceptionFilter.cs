using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SD.Toolkits.AspNet;
using System;
using System.Net;

namespace SD.Toolkits.AspNetCore.Filters
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
            //判断是否是ApiController
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor &&
                actionDescriptor.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true))
            {
                return;
            }

            Exception innerException = GetInnerException(context.Exception);

            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(innerException.Message, ref errorMessage);

            //异常已处理
            context.ExceptionHandled = true;

            //是不是Ajax请求
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                ObjectResult response = new ObjectResult(errorMessage)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.Result = response;
                return;
            }

            //跳转至错误页
            if (!string.IsNullOrWhiteSpace(AspNetSection.Setting.ErrorPage.Value))
            {
                context.HttpContext.Response.Redirect(AspNetSection.Setting.ErrorPage.Value);
            }
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
