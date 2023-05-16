using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Mapper.Tests.StubDTOs;
using SD.Toolkits.Mapper.Tests.StubEntities;
using System;
using System.Diagnostics;

namespace SD.Toolkits.Mapper.Tests.TestCases
{
    /// <summary>
    /// 映射测试
    /// </summary>
    [TestClass]
    public class MapperTests
    {
        #region # 测试正常映射 —— void TestMapNormally()
        /// <summary>
        /// 测试正常映射
        /// </summary>
        [TestMethod]
        public void TestMapNormally()
        {
            for (int index = 0; index < 100000; index++)
            {
                Student student = new Student { Id = 1, Name = "张三", BirthDay = DateTime.Now };
                StudentInfo studentInfo = student.Map<Student, StudentInfo>();

                Assert.IsTrue(studentInfo.Name == student.Name);
            }
        }
        #endregion

        #region # 测试映射后事件执行次数 —— void TestMapAfterMap()
        /// <summary>
        /// 测试映射后事件执行次数
        /// </summary>
        [TestMethod]
        public void TestMapAfterMap()
        {
            Student student = new Student { Id = 1, Name = "张三", BirthDay = DateTime.Now };
            StudentInfo studentInfo = null;
            for (int i = 0; i < 10; i++)
            {
                studentInfo = student.Map<Student, StudentInfo>(null, (source, target) => Trace.WriteLine(DateTime.Now));
            }

            Assert.IsTrue(studentInfo.Name == student.Name);
        }
        #endregion
    }
}
