using System;
using System.Runtime.Serialization;

namespace SD.Toolkits.Serialization.Tests.StubEntities
{
    /// <summary>
    /// 订单数据传输对象
    /// </summary>
    [DataContract]
    public class OrderInfo
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string Number { get; set; }

        /// <summary>
        /// 订单名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}
