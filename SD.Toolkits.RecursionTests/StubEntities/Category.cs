using System;
using System.Collections.Generic;
using System.Linq;
using SD.Toolkits.Recursion;
using SD.Toolkits.Recursion.Tree;

namespace SD.Toolkits.RecursionTests.StubEntities
{
    /// <summary>
    /// 品类
    /// </summary>
    public class Category : ITree<Category>
    {
        #region # 构造器

        #region 01.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Category()
        {
            //初始化导航属性
            this.SubNodes = new HashSet<Category>();

            //默认值
            this.Id = Guid.NewGuid();
        }
        #endregion

        #region 02.创建品类构造器
        /// <summary>
        /// 创建品类构造器
        /// </summary>
        /// <param name="categoryName">品类名称</param>
        /// <param name="parentCategory">上级品类</param>
        public Category(string categoryName, Category parentCategory)
            : this()
        {
            this.Name = categoryName;
            this.ParentNode = parentCategory;

            if (this.ParentNode != null)
            {
                this.ParentNode.SubNodes.Add(this);
            }
        }
        #endregion

        #endregion

        #region # 属性

        #region 品类Id —— Guid Id
        /// <summary>
        /// 品类Id
        /// </summary>
        public Guid Id { get; private set; }
        #endregion

        #region 品类名称 —— string Name
        /// <summary>
        /// 品类名称
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region 只读属性 - 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 只读属性 - 是否是根级节点
        /// </summary>
        public bool IsRoot
        {
            get { return this.ParentNode == null; }
        }
        #endregion

        #region 只读属性 - 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 只读属性 - 是否是叶子级节点
        /// </summary>
        public bool IsLeaf
        {
            get { return !this.SubNodes.Any(); }
        }
        #endregion

        #region 导航属性 - 父节点 —— Category ParentNode
        /// <summary>
        /// 导航属性 - 父节点
        /// </summary>
        public virtual Category ParentNode { get; private set; }
        #endregion

        #region 导航属性 - 子节点集 —— ICollection<Category> SubNodes
        /// <summary>
        /// 导航属性 - 子节点集
        /// </summary>
        public virtual ICollection<Category> SubNodes { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 添加子品类 —— void AddSubCategory(Category subCategory)
        /// <summary>
        /// 添加子品类
        /// </summary>
        /// <param name="subCategory">子品类</param>
        public void AddSubCategory(Category subCategory)
        {
            this.SubNodes.Add(subCategory);
            subCategory.ParentNode = this;
        }
        #endregion

        #endregion
    }
}
