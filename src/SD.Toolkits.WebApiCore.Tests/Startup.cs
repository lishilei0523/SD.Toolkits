using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SD.Toolkits.WebApiCore.Tests
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

            services.AddControllers();
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
