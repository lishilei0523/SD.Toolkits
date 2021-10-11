using System;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base
{
    /// <summary>
    /// 聚合根实体基类
    /// </summary>
    [Serializable]
    public abstract class AggregateRootEntity : PlainEntity
    {
        #region 编号 —— string Number
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; protected set; }
        #endregion

        #region 名称 —— string Name
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; protected set; }
        #endregion
    }
}
