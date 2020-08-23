using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
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
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            IServiceCollection builder = ResolveMediator.GetServiceCollection();
            builder.RegisterConfigs();

            ResolveMediator.Build();
        }

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
