using System;

namespace SD.Common
{
    /// <summary>
    /// 断言失败异常
    /// </summary>
    [Serializable]
    public class AssertFailedException : ApplicationException
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public AssertFailedException() { }

        /// <summary>
        /// 基础构造器
        /// </summary>
        /// <param name="message">异常消息</param>
        public AssertFailedException(string message)
            : base(message)
        {

        }
    }
}
