using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SD.Toolkits.AspNetCore.Filters;

namespace SD.Toolkits.AspNetCore.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //���Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "WebApi �ӿ��ĵ�"
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new WebApiExceptionFilter());
            });
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
            //����Swagger�м��
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1.0/swagger.json", "WebApi �ӿ��ĵ� v1.0");
            });

            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
