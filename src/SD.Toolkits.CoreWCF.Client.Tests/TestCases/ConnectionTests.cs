using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Extensions;

namespace SD.Toolkits.CoreWCF.Client.Tests.TestCases
{
    /// <summary>
    /// 连接测试
    /// </summary>
    [TestClass]
    public class ConnectionTests
    {
        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 用户服务契约接口
        /// </summary>
        private IUserContract _userContract;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            ServiceModelSectionGroup.Initialize(configuration);

            ServiceProxy<IAuthenticationContract> authenticationContractProxy = new ServiceProxy<IAuthenticationContract>();
            ServiceProxy<IUserContract> userContractProxy = new ServiceProxy<IUserContract>();
            this._authenticationContract = authenticationContractProxy.Channel;
            this._userContract = userContractProxy.Channel;
        }

        /// <summary>
        /// 测试登录
        /// </summary>
        [TestMethod]
        public void TestLogin()
        {
            LoginInfo loginInfo = this._authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);

            Trace.WriteLine("公钥：");
            Trace.WriteLine(loginInfo.PublicKey);
            Trace.WriteLine("----------------------------------");

            //存储登录信息
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);
        }

        /// <summary>
        /// 测试获取用户列表
        /// </summary>
        [TestMethod]
        public void TestGetUsers()
        {
            LoginInfo loginInfo = this._authenticationContract.Login(CommonConstants.AdminLoginId, CommonConstants.InitialPassword);
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);

            IEnumerable<UserInfo> users = this._userContract.GetUsers(null, null, null);

            Trace.WriteLine("用户列表：");
            Trace.WriteLine("----------------------------------");
            foreach (UserInfo userInfo in users)
            {
                Trace.WriteLine($"用户名：{userInfo.Number}");
                Trace.WriteLine($"真实姓名：{userInfo.Name}");
                Trace.WriteLine("----");
            }
        }
    }
}
