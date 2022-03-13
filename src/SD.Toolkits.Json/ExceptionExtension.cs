using Newtonsoft.Json;
using System;
using System.Collections;

namespace SD.Toolkits.Json
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static class ExceptionExtension
    {
        //Public

        #region # 获取内部异常 —— static Exception GetInnerException(this Exception exception)
        /// <summary>
        /// 获取内部异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>内部异常</returns>
        public static Exception GetInnerException(this Exception exception)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                {
                    return exception.InnerException.GetInnerException();
                }

                return exception;
            }

            return null;
        }
        #endregion

        #region # 获取异常消息 —— static string GetErrorMessage(this Exception exception)
        /// <summary>
        /// 获取异常消息
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns>异常消息</returns>
        public static string GetErrorMessage(this Exception exception)
        {
            string errorMessage = string.Empty;
            Exception innerException = exception.GetInnerException();
            if (innerException != null)
            {
                errorMessage = GetErrorMessage(innerException.Message, ref errorMessage);
                return errorMessage;
            }

            return errorMessage;
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
                IDictionary json = JsonConvert.DeserializeObject<IDictionary>(exceptionMessage);
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
