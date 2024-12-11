using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;

namespace SD.Toolkits.AspNetCore.Extensions
{
    /// <summary>
    /// HTTP请求扩展
    /// </summary>
    public static class HttpRequestExtension
    {
        #region # 判断是否是Ajax请求 —— static bool IsAjaxRequest(this HttpRequest request)
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <returns>是否是Ajax请求</returns>
        public static bool IsAjaxRequest(this HttpRequest request)
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

        #region # Controller/Action是否有某特性标签 —— static bool HasAttr<T>(this ActionDescriptor...
        /// <summary>
        /// Controller/Action是否有某特性标签
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="actionDescriptor">Action方法元数据</param>
        /// <returns>是否拥有该特性</returns>
        public static bool HasAttr<T>(this ActionDescriptor actionDescriptor) where T : Attribute
        {
            Type type = typeof(T);
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                bool actionDefined = controllerActionDescriptor.MethodInfo.IsDefined(type, true);
                if (actionDefined)
                {
                    return true;
                }

                bool controllerDefined = controllerActionDescriptor.ControllerTypeInfo.IsDefined(type, true);
                if (controllerDefined)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }
        #endregion
    }
}
