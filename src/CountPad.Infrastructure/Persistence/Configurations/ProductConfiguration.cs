using CountPad.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Navigation(p => p.ProductCategory).AutoInclude();
		}
	}
}
