using SD.Toolkits.EntityFramework.Tests.StubEntities.Base;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.EntityFramework.Tests.StubEntities
{
    /// <summary>
    /// 单据
    /// </summary>
    public class Order : AggregateRootEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Order()
        {
            //初始化导航属性
            this.Details = new HashSet<OrderDetail>();
        }
        #endregion

        #region 01.创建单据构造器
        /// <summary>
        /// 创建单据构造器
        /// </summary>
        /// <param name="orderNo">单据编号</param>
        /// <param name="orderName">单据名称</param>
        /// <param name="description">描述</param>
        public Order(string orderNo, string orderName, string description)
            : this()
        {
            base.Number = orderNo;
            base.Name = orderName;
            this.Description = description;
        }
        #endregion

        #endregion

        #region # 属性

        #region 描述 —— string Description
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
        #endregion

        #region 导航属性 - 单据明细集 —— ICollection<OrderDetail> Details
        /// <summary>
        /// 导航属性 - 单据明细集
        /// </summary>
        public virtual ICollection<OrderDetail> Details { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 设置单据明细 —— void SetDetails(IEnumerable<OrderDetail> details)
        /// <summary>
        /// 设置单据明细
        /// </summary>
        /// <param name="details">单据明细集</param>
        public void SetDetails(IEnumerable<OrderDetail> details)
        {
            details = details?.ToArray() ?? new OrderDetail[0];

            this.ClearDetails();
            if (details.Any())
            {
                foreach (OrderDetail detail in details)
                {
                    this.Details.Add(detail);
                    detail.Order = this;
                }
            }
        }
        #endregion

        #region 清空单据明细 —— void ClearDetails()
        /// <summary>
        /// 清空单据明细
        /// </summary>
        private void ClearDetails()
        {
            foreach (OrderDetail detail in this.Details.ToArray())
            {
                this.Details.Remove(detail);
            }
        }
        #endregion

        #endregion
    }
}
