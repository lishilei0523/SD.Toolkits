using SD.Toolkits.EntityFramework.Extensions;
using SD.Toolkits.EntityFramework.Tests.StubEntities;
using System.Data.Entity.ModelConfiguration;

namespace SD.Toolkits.EntityFramework.Tests.EntityConfigurations
{
    /// <summary>
    /// 学生实体配置
    /// </summary>
    public class StudentConfig : EntityTypeConfiguration<Student>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public StudentConfig()
        {
            //创建Student表ClassId的非聚集索引
            this.HasIndex("IX_ClassId", IndexType.Nonclustered, table => table.Property(stu => stu.ClassId));
        }
    }
}
