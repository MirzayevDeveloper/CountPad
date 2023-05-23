using CountPad.Application.Common.Interfaces;
using CountPad.Infrastructure.Persistence;
using CountPad.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CountPad.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
			{
				options.UseNpgsql(connectionString: configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
			services.AddTransient<IDateTime, DateTimeService>();

			return services;
		}
	}
}
