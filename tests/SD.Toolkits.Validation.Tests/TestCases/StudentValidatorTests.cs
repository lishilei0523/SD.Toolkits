using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Toolkits.Validation.Tests.StubEntities;
using System;

namespace SD.Toolkits.Validation.Tests.TestCases
{
    /// <summary>
    /// 学生验证测试
    /// </summary>
    [TestClass]
    public class StudentValidatorTests
    {
        /// <summary>
        /// 创建学生测试
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidateFailedException))]
        public void CreateStudentTest()
        {
            Student student = new Student(null, null, false, -1, Guid.NewGuid());
            ValidateMediator.Validate(student);
        }
    }
}
