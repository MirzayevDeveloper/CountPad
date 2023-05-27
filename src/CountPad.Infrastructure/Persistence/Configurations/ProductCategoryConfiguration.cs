using CountPad.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.HasIndex(pc => pc.Name)
				.IsUnique();
		}
	}
}
