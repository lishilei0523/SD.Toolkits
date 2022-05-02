using System.Runtime.Serialization;

namespace SD.Toolkits.Grpc.Server.Base
{
    /// <summary>
    /// gRPC响应
    /// </summary>
    [DataContract]
    public class Response<T>
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Response() { }

        /// <summary>
        /// 创建gRPC响应构造器
        /// </summary>
        /// <param name="result">结果</param>
        public Response(T result)
            : this()
        {
            this.Result = result;
        }

        /// <summary>
        /// 结果
        /// </summary>
        [DataMember(Order = 1)]
        public T Result { get; set; }
    }
}
