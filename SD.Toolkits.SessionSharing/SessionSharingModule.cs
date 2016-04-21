using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace SD.Toolkits.SessionSharing
{
    /// <summary>
    /// Session共享HttpModule
    /// </summary>
    internal class SessionSharingModule : IHttpModule
    {
        #region # 常量

        /// <summary>
        /// 应用程序名称
        /// </summary>
        private const string ApplicationName = "UnifedApplication";

        /// <summary>
        /// Session提供者字段名称
        /// </summary>
        private const string StoreField = "_store";

        /// <summary>
        /// 进程外Session类型名称
        /// </summary>
        private const string OutOfProcSessionStateStoreType = "OutOfProcSessionStateStore";

        /// <summary>
        /// uribase字段名称
        /// </summary>
        private const string UribaseField = "s_uribase";

        #endregion

        #region # 初始化 —— void Init(HttpApplication context)
        /// <summary>
        /// 初始化，
        /// 相当于Application_Start事件
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        public void Init(HttpApplication context)
        {
            this.InitSessionSharing(context);
        }
        #endregion

        #region # 初始化Session共享 —— void InitSessionSharing(HttpApplication context...
        /// <summary>
        /// 初始化Session共享
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        private void InitSessionSharing(HttpApplication context)
        {
            //获取Session Module
            IHttpModule sessionModule = this.GetSessionModule(context);

            //获取Session存储提供者字段
            FieldInfo storeField = sessionModule.GetType().GetField(StoreField, BindingFlags.Instance | BindingFlags.NonPublic);
            if (storeField == null) return;

            //获取Session存储提供者实例
            object sessionStoreProvider = storeField.GetValue(sessionModule);
            if (sessionStoreProvider == null) return;

            //获取Session存储提供者类型
            Type storeProviderType = sessionStoreProvider.GetType();
            if (storeProviderType.Name != OutOfProcSessionStateStoreType) return;

            //进程外Session
            FieldInfo uribaseField = storeProviderType.GetField(UribaseField, BindingFlags.Static | BindingFlags.NonPublic);
            if (uribaseField == null) return;

            //设置应用程序名称
            uribaseField.SetValue(storeProviderType, ApplicationName);
        }
        #endregion

        #region # 获取Session Module —— IHttpModule GetSessionModule(HttpApplication...
        /// <summary>
        /// 获取Session Module
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        /// <returns>Session Module</returns>
        private IHttpModule GetSessionModule(HttpApplication context)
        {
            //获取所有HttpModule
            IEnumerable<IHttpModule> httpModules =
                from string moduleName in context.Modules
                select context.Modules[moduleName];

            return httpModules.Single(x => x is SessionStateModule);
        }
        #endregion

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源，
        /// 相当于Application_End事件
        /// </summary>
        public void Dispose() { }
        #endregion
    }
}
