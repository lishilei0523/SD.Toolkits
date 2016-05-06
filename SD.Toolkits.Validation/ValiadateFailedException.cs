using System;

namespace SD.Toolkits.Validation
{
    /// <summary>
    /// 验证失败异常
    /// </summary>
    [Serializable]
    public sealed class ValiadateFailedException : ApplicationException
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public ValiadateFailedException() { }

        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="message">异常消息</param>
        public ValiadateFailedException(string message)
            : base(message)
        {

        }
    }
}
