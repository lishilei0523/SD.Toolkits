using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SD.Toolkits.WebApiCore.Filters;

namespace SD.Toolkits.WebApiCore.Tests
{
    public class Startup
    {
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
            });
        }

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
