using Microsoft.AspNetCore.Http;
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
    }
}
