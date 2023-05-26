using CountPad.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasIndex(u => u.Phone)
				.IsUnique();

			builder.Navigation(u => u.Roles).AutoInclude();
		}
	}
}
