using CountPad.Domain.Entities.Packages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class PackageConfiguration : IEntityTypeConfiguration<Package>
	{
		public void Configure(EntityTypeBuilder<Package> builder)
		{
			builder.Navigation(p => p.Product).AutoInclude();
			builder.Navigation(p => p.Distributor).AutoInclude();
		}
	}
}
