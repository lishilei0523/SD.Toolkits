using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.Base;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities
{
    /// <summary>
    /// 单据明细
    /// </summary>
    public class OrderDetail : PlainEntity
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected OrderDetail() { }
        #endregion

        #region 01.创建单据明细构造器
        /// <summary>
        /// 创建单据明细构造器
        /// </summary>
        public OrderDetail(string product, decimal quantity)
            : this()
        {
            this.Product = product;
            this.Quantity = quantity;
        }
        #endregion

        #endregion

        #region # 属性

        #region 产品 —— string Product
        /// <summary>
        /// 产品
        /// </summary>
        public string Product { get; private set; }
        #endregion

        #region 数量 —— decimal Quantity
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; private set; }
        #endregion

        #region 导航属性 - 单据 —— Order Order
        /// <summary>
        /// 导航属性 - 单据
        /// </summary>
        public virtual Order Order { get; internal set; }
        #endregion

        #endregion
    }
}
