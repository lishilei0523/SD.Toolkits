using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Text.Json;

namespace SD.Toolkits.WebApiCore.Filters
{
    /// <summary>
    /// ASP.NET Core WebApi异常过滤器
    /// </summary>
    public class WebApiExceptionFilter : IExceptionFilter
    {
        //Implements

        #region # 执行异常事件过滤器 —— void OnException(ExceptionContext context)
        /// <summary>
        /// 执行异常事件过滤器
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            //判断是否是ApiController
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor &&
                actionDescriptor.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true))
            {
                Exception innerException = GetInnerException(context.Exception);

                //处理异常消息
                string errorMessage = string.Empty;
                errorMessage = GetErrorMessage(innerException.Message, ref errorMessage);

                ObjectResult response = new ObjectResult(errorMessage)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                context.Result = response;
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
    }
}