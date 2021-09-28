using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SD.Toolkits.CoreWCF.Client.Tests.TestCases
{
    /// <summary>
    /// 连接测试
    /// </summary>
    public class ConnectionTests
    {
        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private readonly IUserContract _userContract;

        /// <summary>
        /// 构造器
        /// </summary>
        public ConnectionTests()
        {
            ServiceProxy<IAuthenticationContract> authenticationContractProxy = new ServiceProxy<IAuthenticationContract>();
            ServiceProxy<IUserContract> userContractProxy = new ServiceProxy<IUserContract>();
            this._authenticationContract = authenticationContractProxy.Channel;
            this._userContract = userContractProxy.Channel;
        }

        /// <summary>
        /// 测试登录
        /// </summary>
        public void TestLogin()
        {
            LoginInfo loginInfo = this._authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            Console.WriteLine("公钥：");
            Console.WriteLine(loginInfo.PublicKey);
            Console.WriteLine("----------------------------------");

            //存储登录信息
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);
        }

        /// <summary>
        /// 测试获取用户列表
        /// </summary>
        public void TestGetUsers()
        {
            IEnumerable<UserInfo> users = this._userContract.GetUsers(null);

            Console.WriteLine("用户列表：");
            Console.WriteLine("----------------------------------");
            foreach (UserInfo userInfo in users)
            {
                Console.WriteLine($"用户名：{userInfo.Number}");
                Console.WriteLine($"真实姓名：{userInfo.Name}");
                Console.WriteLine("----");
            }
        }
    }
}
