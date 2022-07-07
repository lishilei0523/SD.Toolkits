using SD.Toolkits.AspNet;
using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SD.Toolkits.AspNetMvc.Filters
{
    /// <summary>
    /// ASP.NET MVC异常过滤器
    /// </summary>
    public class MvcExceptionFilter : IExceptionFilter
    {
        #region # 字段及构造器

        /// <summary>
        /// JSON序列化器
        /// </summary>
        private static readonly JavaScriptSerializer _JsonSerializer;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static MvcExceptionFilter()
        {
            _JsonSerializer = new JavaScriptSerializer();
        }

        #endregion


        //Implements

        #region # 执行异常过滤器事件 —— void OnException(ExceptionContext context)
        /// <summary>
        /// 执行异常过滤器事件
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            Exception innerException = GetInnerException(context.Exception);

            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = GetErrorMessage(innerException.Message, ref errorMessage);

            //设置状态码为500
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //IIS处理
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Ajax请求
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                //响应
                context.HttpContext.Response.Write(errorMessage);
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
                context.HttpContext.Response.Write(scriptBuilder.ToString());
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
                IDictionary json = _JsonSerializer.DeserializeObject(exceptionMessage) as IDictionary;
                if (json != null && json.Contains("ErrorMessage"))
                {
                    errorMessage = json["ErrorMessage"].ToString();
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