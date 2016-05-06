using System;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using SD.IOC.Core.Mediator;

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
            //获取验证者实例
            IValidator<T> validator = ResolveMediator.ResolveOptional<IValidator<T>>();

            #region # 验证类型是否已注册

            if (validator == null)
            {
                throw new NullReferenceException(string.Format("未找到类型{0}的验证配置，请检查程序！", typeof(T).Name));
            }

            #endregion

            //得到验证结果
            ValidationResult validationResult = validator.Validate(instance);

            //如果未验证通过
            if (!validationResult.IsValid)
            {
                //构造异常消息
                StringBuilder builder = new StringBuilder();

                foreach (string message in validationResult.Errors.Select(x => x.ErrorMessage))
                {
                    builder.Append(message);
                    builder.Append("/");
                }

                string errorMessaage = builder.ToString().Substring(0, builder.Length - 1);
                throw new ValidateFailedException(errorMessaage);
            }
        }
    }
}
