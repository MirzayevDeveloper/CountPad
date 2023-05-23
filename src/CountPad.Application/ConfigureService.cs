using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CountPad.Application
{
	public static class ConfigureService
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			return services;
		}
	}
}
