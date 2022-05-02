using System.Runtime.Serialization;

namespace SD.Toolkits.Grpc.Server.Base
{
    /// <summary>
    /// gRPC请求
    /// </summary>
    [DataContract]
    public class Request<T>
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Request() { }

        /// <summary>
        /// 创建gRPC请求构造器
        /// </summary>
        /// <param name="param">参数</param>
        public Request(T param)
            : this()
        {
            this.Param = param;
        }

        /// <summary>
        /// 参数
        /// </summary>
        [DataMember(Order = 1)]
        public T Param { get; set; }
    }
}
