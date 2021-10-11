using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SD.Toolkits.EntityFrameworkCore.Tests.StubEntities.EntityConfigurations
{
    /// <summary>
    /// 单据明细实体配置
    /// </summary>
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.Product).IsRequired();
        }
    }
}
