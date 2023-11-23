using SD.Toolkits.Recursion.Tree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Recursion.Tests.StubEntities
{
    /// <summary>
    /// 类别
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

        #region 02.创建类别构造器
        /// <summary>
        /// 创建类别构造器
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="parentNode">上级节点</param>
        public Category(string categoryName, Category parentNode)
            : this()
        {
            this.Name = categoryName;
            this.ParentNode = parentNode;
            parentNode?.SubNodes.Add(this);
        }
        #endregion

        #endregion

        #region # 属性

        #region 类别Id —— Guid Id
        /// <summary>
        /// 类别Id
        /// </summary>
        public Guid Id { get; private set; }
        #endregion

        #region 类别名称 —— string Name
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region 只读属性 - 是否是根级节点 —— bool IsRoot
        /// <summary>
        /// 只读属性 - 是否是根级节点
        /// </summary>
        public bool IsRoot
        {
            get => this.ParentNode == null;
        }
        #endregion

        #region 只读属性 - 是否是叶子级节点 —— bool IsLeaf
        /// <summary>
        /// 只读属性 - 是否是叶子级节点
        /// </summary>
        public bool IsLeaf
        {
            get => !this.SubNodes.Any();
        }
        #endregion

        #region 导航属性 - 上级节点 —— Category ParentNode
        /// <summary>
        /// 导航属性 - 上级节点
        /// </summary>
        public virtual Category ParentNode { get; private set; }
        #endregion

        #region 导航属性 - 下级节点集 —— ICollection<Category> SubNodes
        /// <summary>
        /// 导航属性 - 下级节点集
        /// </summary>
        public virtual ICollection<Category> SubNodes { get; private set; }
        #endregion

        #endregion

        #region # 方法

        #region 添加下级节点 —— void AddSubNode(Category category)
        /// <summary>
        /// 添加下级节点
        /// </summary>
        /// <param name="category">类别</param>
        public void AddSubNode(Category category)
        {
            this.SubNodes.Add(category);
            category.ParentNode = this;
        }
        #endregion

        #region 重写ToString方法 —— override string ToString()
        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>名称</returns>
        public override string ToString()
        {
            return this.Name;
        }
        #endregion

        #endregion
    }
}
