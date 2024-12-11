using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SD.Toolkits.AspNetCore.Extensions
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    internal static class ExceptionExtension
    {
        #region # 递归获取内部异常 —— static Exception GetInnerException(this Exception exception)
        /// <summary>
        /// 递归获取内部异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>内部异常</returns>
        internal static Exception GetInnerException(this Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnerException(exception.InnerException);
            }

            return exception;
        }
        #endregion

        #region # 递归获取错误消息 —— static string GetErrorMessage( thisstring exceptionMessage...
        /// <summary>
        /// 递归获取错误消息
        /// </summary>
        /// <param name="exceptionMessage">异常消息</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>错误消息</returns>
        internal static string GetErrorMessage(this string exceptionMessage, ref string errorMessage)
        {
            try
            {
                const string errorMessageKey = "ErrorMessage";
                IDictionary<string, object> jObject = JsonSerializer.Deserialize<IDictionary<string, object>>(exceptionMessage);
                if (jObject != null && jObject.TryGetValue(errorMessageKey, out object value))
                {
                    errorMessage = value?.ToString();
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
