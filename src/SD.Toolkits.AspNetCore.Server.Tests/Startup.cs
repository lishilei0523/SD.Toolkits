using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SD.Toolkits.AspNetCore.Filters;
using System.Text.Json;

namespace SD.Toolkits.AspNetCore.Server.Tests
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "WebApi 接口文档"
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new WebApiExceptionFilter());
            }).AddJsonOptions(options =>
            {
                //Camel命名设置
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置Swagger中间件
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "WebApi 接口文档 v1.0");
            });

            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
