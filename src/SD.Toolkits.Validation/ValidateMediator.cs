using FluentValidation;
using FluentValidation.Results;
using SD.IOC.Core.Mediators;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SD.Toolkits.Validation
{
    /// <summary>
    /// 验证中介者
    /// </summary>
    public static class ValidateMediator
    {
        /// <summary>
        /// 验证实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="instance">实例对象</param>
        public static void Validate<T>(T instance)
        {
            //获取验证者实例集
            IEnumerable<IValidator<T>> validators = ResolveMediator.ResolveAll<IValidator<T>>();

            //构造异常消息
            StringBuilder builder = new StringBuilder();

            foreach (IValidator<T> validator in validators)
            {
                //得到验证结果
                ValidationResult validationResult = validator.Validate(instance);

                //如果未验证通过
                if (!validationResult.IsValid)
                {
                    foreach (string message in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        builder.Append(message);
                        builder.Append("/");
                    }
                }
            }

            //说明验证未通过
            if (builder.Length > 0)
            {
                string errorMessaage = builder.ToString().Substring(0, builder.Length - 1);
                throw new ValidateFailedException(errorMessaage);
            }
        }
    }
}
