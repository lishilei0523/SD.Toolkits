﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace SD.Toolkits.WebApiCore.Filters
{
    /// <summary>
    /// WebApi异常过滤器
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
            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(context.Exception.Message, ref errorMessage);

            ObjectResult response = new ObjectResult(errorMessage)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.Result = response;
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