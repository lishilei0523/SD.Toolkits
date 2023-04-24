# 通用扩展组件

##### 2019.03 项目近期调整说明

	1、定时任务相关组件集成到SD.Framework中，不再作为工具类库单独使用；

	2、增加HttpContext异步环境支持（HttpContext.Current在异步/多线程环境下为null）；

##### 2018.04 项目近期调整说明
	
	1、除 SD.Common.NetFx 与 SD.Toolkits.EntityFramework 外，其余项目全部使用 .NET Standard 2.0 重写；

	2、SD.Toolkits.Redis 采用 StackExchange.Redis 重新实现；

	3、SD.Toolkits.NoGenerator 更名为 SD.Toolkits.SerialNumber ；

	4、将图像处理部分剥离出 SD.Common，新增 SD.Toolkits.Image ；

#### 包含

	通用基础类库；
	
	EntityFramework 类表映射扩展、索引生成扩展、IQueryable 扩展；
	
	EntityFramework Core 类表映射扩展；

	Excel 读写组件，基于 NPOI ；

	图像处理、验证码组件，基于 ZKWeb.System.Drawing ；

	递归扩展组件；

	Redis 管理组件，基于 StackExchange.Redis ；

	编号生成器组件，基于 ADO.NET + SQL Server ；

	实体验证工具，基于 FluentValidation ；

    Microsoft.Web.RedisSessionStateProvider 测试用例（Redis Session 存储，多网站 Session 共享）；