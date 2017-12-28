using System;

namespace SD.Toolkits.Validation.Tests.StubEntities
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        protected Student() { }

        /// <summary>
        /// 创建学生构造器
        /// </summary>
        /// <param name="studentNo">学生编号</param>
        /// <param name="studentName">学生名称</param>
        /// <param name="gender">性别</param>
        /// <param name="age">年龄</param>
        /// <param name="classId">班级Id</param>
        public Student(string studentNo, string studentName, bool gender, float age, Guid classId)
            : this()
        {
            this.Id = Guid.NewGuid();
            this.Number = studentNo;
            this.Name = studentName;
            this.Gender = gender;
            this.Age = age;
            this.ClassId = classId;
        }

        /// <summary>
        /// 标识Id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; private set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public float Age { get; private set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid ClassId { get; private set; }
    }
}
