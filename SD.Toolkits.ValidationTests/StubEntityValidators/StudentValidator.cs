using FluentValidation;
using SD.Toolkits.ValidationTests.StubEntities;

namespace SD.Toolkits.ValidationTests.StubEntityValidators
{
    /// <summary>
    /// 学生验证者
    /// </summary>
    public class StudentValidator : AbstractValidator<Student>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public StudentValidator()
        {
            this.RuleFor(x => x.Name).NotNull().WithMessage("名称不可为空！");
            this.RuleFor(x => x.Age).GreaterThan(0).WithMessage("年龄必须大于0！");
        }
    }
}
