using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Recursion.Tests.StubEntities;
using SD.Toolkits.Recursion.Tree;
using System.Collections.Generic;
using System.Linq;

namespace SD.Toolkits.Recursion.Tests.TestCases
{
    /// <summary>
    /// 树形递归扩展测试
    /// </summary>
    [TestClass]
    public class TreeRecursionExtensionTests
    {
        /// <summary>
        /// 品类集
        /// </summary>
        private ICollection<Category> _categories;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this._categories = new HashSet<Category>();

            Category area1 = new Category("一级类目1", null);
            Category area2 = new Category("一级类目2", null);

            Category kind1 = new Category("二级类目1", area1);
            Category kind2 = new Category("二级类目2", area1);
            Category kind3 = new Category("二级类目3", area2);
            Category kind4 = new Category("二级类目4", area2);

            Category category1 = new Category("品类1", kind1);
            Category category2 = new Category("品类2", kind1);
            Category category3 = new Category("品类3", kind2);
            Category category4 = new Category("品类4", kind2);
            Category category5 = new Category("品类5", kind3);
            Category category6 = new Category("品类6", kind3);
            Category category7 = new Category("品类7", kind4);
            Category category8 = new Category("品类8", kind4);

            this._categories.Add(area1);
            this._categories.Add(area2);
            this._categories.Add(kind1);
            this._categories.Add(kind2);
            this._categories.Add(kind3);
            this._categories.Add(kind4);
            this._categories.Add(category1);
            this._categories.Add(category2);
            this._categories.Add(category3);
            this._categories.Add(category4);
            this._categories.Add(category5);
            this._categories.Add(category6);
            this._categories.Add(category7);
            this._categories.Add(category8);
        }

        /// <summary>
        /// 深度获取上级节点集测试
        /// </summary>
        [TestMethod]
        public void GetDeepParentNodesTest()
        {
            Category category = this._categories.Single(x => x.Name == "品类8");

            IEnumerable<Category> parentCategories = category.GetDeepParentNodes();

            Assert.IsTrue(parentCategories.Count() == 2);
        }

        /// <summary>
        /// 深度获取下级节点集测试
        /// </summary>
        [TestMethod]
        public void GetDeepSubNodesTest()
        {
            Category area1 = this._categories.Single(x => x.Name == "一级类目1");

            IEnumerable<Category> collection = area1.GetDeepSubNodes();

            Assert.IsTrue(collection.Count() == 6);
        }

        /// <summary>
        /// 尾递归上级扩展方法测试
        /// </summary>
        [TestMethod]
        public void TailRecursionParentNodesTest()
        {
            IEnumerable<Category> categories = this._categories.Where(x => x.IsLeaf);

            IEnumerable<Category> collection = categories.TailRecurseParentNodes();

            Assert.IsTrue(collection.Count() == 14);
        }

        /// <summary>
        ///获取树路径扩展方法测试
        /// </summary>
        [TestMethod]
        public void GetTreePathTest()
        {
            string expectedPath = "/一级类目2/二级类目4/品类8";

            Category category = this._categories.Single(x => x.Name == "品类8");

            string treePath = category.GetTreePath();

            Assert.IsTrue(treePath == expectedPath);
        }
    }
}
