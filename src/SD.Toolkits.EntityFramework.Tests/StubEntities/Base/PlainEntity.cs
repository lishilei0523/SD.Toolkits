using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SD.Toolkits.EntityFramework.Tests.StubEntities.Base
{
    /// <summary>
    /// 领域实体基类
    /// </summary>
    [Serializable]
    public abstract class PlainEntity
    {
        #region # 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        protected PlainEntity()
        {
            this.Id = Guid.NewGuid();
            this.AddedTime = DateTime.Now;
        }
        #endregion

        #region # 属性

        #region 标识 —— Guid Id
        /// <summary>
        /// 标识
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; protected set; }
        #endregion

        #region 添加时间 —— DateTime AddedTime
        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddedTime { get; private set; }
        #endregion

        #endregion
    }
}
