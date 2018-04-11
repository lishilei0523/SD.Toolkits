using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.Toolkits.EntityFrameworkCore.Tests.StubEntities;

namespace SD.Toolkits.EntityFrameworkCore.Tests.EntityConfigurations
{
    /// <summary>
    /// 学生实体配置
    /// </summary>
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(32);
        }
    }
}
