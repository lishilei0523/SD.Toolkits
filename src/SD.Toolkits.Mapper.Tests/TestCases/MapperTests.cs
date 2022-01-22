using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Mapper.Tests.StubDTOs;
using SD.Toolkits.Mapper.Tests.StubEntities;
using System;
using System.Diagnostics;

namespace SD.Toolkits.Mapper.Tests.TestCases
{
    /// <summary>
    /// 映射工具测试类
    /// </summary>
    [TestClass]
    public class MapperTests
    {
        /// <summary>
        /// 测试正常映射
        /// </summary>
        [TestMethod]
        public void TestMap_Normal()
        {
            for (int index = 0; index < 100000; index++)
            {
                Student student = new Student { Id = 1, Name = "张三", BirthDay = DateTime.Now };
                StudentInfo studentInfo = student.Map<Student, StudentInfo>();

                Assert.IsTrue(studentInfo.Name == student.Name);
            }
        }

        /// <summary>
        /// 测试映射后事件执行次数
        /// </summary>
        [TestMethod]
        public void TestMap_AfterMap()
        {
            Student student = new Student { Id = 1, Name = "张三", BirthDay = DateTime.Now };

            StudentInfo studentInfo = null;
            for (int i = 0; i < 10; i++)
            {
                studentInfo = student.Map<Student, StudentInfo>(null, this.AfterMap);
            }

            Assert.IsTrue(studentInfo.Name == student.Name);
        }

        /// <summary>
        /// 映射后事件方法
        /// </summary>
        /// <param name="source">源实例</param>
        /// <param name="target">目标实例</param>
        private void AfterMap(Student source, StudentInfo target)
        {
            Trace.WriteLine(DateTime.Now);
        }
    }
}
