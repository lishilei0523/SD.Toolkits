# 通用扩展工具

##### 2018.04 项目近期调整说明
	
	1、除SD.Common.NetFx与SD.Toolkits.EntityFramework外，其余项目全部使用 .NET Standard 2.0 重写；

	2、SD.Toolkits.Redis 采用 StackExchange.Redis 重新实现；

	3、SD.Toolkits.NoGenerator 更名为 SD.Toolkits.SerialNumber ；

	4、将图像处理部分剥离出 SD.Common，新增 SD.Toolkits.Image ；

包含

	通用基础类库
	
	EntityFramework类表映射工具、索引生成工具、IQueryable扩展工具；
	
	EntityFramework Core类表映射工具；

	Excel读写工具，基于 NPOI ；

	图像处理、验证码工具，基于 ZKWeb.System.Drawing ；

	递归扩展工具；

	Redis管理工具，基于 StackExchange.Redis；

	编号生成器工具，基于 ADO.NET + SQL Server；

    定时任务调度扩展工具，基于 Quartz.NET ；

	实体验证工具，基于 FluentValidation；

    Microsoft.Web.RedisSessionStateProvider 测试用例（Redis Session 存储，多网站 Session 共享）；