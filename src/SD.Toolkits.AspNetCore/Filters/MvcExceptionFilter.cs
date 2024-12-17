using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SD.Toolkits.AspNet;
using SD.Toolkits.AspNetCore.Extensions;
using System;
using System.Net;

namespace SD.Toolkits.AspNetCore.Filters
{
    /// <summary>
    /// ASP.NET Core MVC异常过滤器
    /// </summary>
    public class MvcExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 发生异常事件
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            //判断是否是ApiController
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor &&
                actionDescriptor.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true))
            {
                return;
            }

            //获取内部异常
            Exception innerException = context.Exception.GetInnerException();

            //处理异常消息
            string errorMessage = string.Empty;
            errorMessage = innerException.Message.GetErrorMessage(ref errorMessage);

            //异常已处理
            context.ExceptionHandled = true;

            //是不是Ajax请求
            if (context.HttpContext.Request.IsAjaxRequest())
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
    }
}
