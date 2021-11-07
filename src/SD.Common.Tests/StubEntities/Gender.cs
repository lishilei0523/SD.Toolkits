using System.ComponentModel;
using System.Runtime.Serialization;

namespace SD.Common.Tests.StubEntities
{
    /// <summary>
    /// 性别
    /// </summary>
    [DataContract]
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        [EnumMember]
        Male = 0,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        [EnumMember]
        Female = 1
    }
}
