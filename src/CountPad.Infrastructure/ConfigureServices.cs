using System.Text;
using System;
using CountPad.Application.Common.Interfaces;
using CountPad.Infrastructure.Persistence;
using CountPad.Infrastructure.Persistence.Interceptors;
using CountPad.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

			services.AddScoped<AuditableEntitySaveChangesInterceptor>();
			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			services.AddTransient<IDateTime, DateTimeService>();
			services.AddTransient<IGuidGenerator, GuidGeneratorService>();
			services.AddTransient<ISecurityService, SecurityService>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					string key = configuration.GetSection("Jwt").GetValue<string>("Key");
					string audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
					string issuer = configuration.GetSection("Jwt").GetValue<string>("Issuer");
					byte[] convertKeyToBytes = Encoding.UTF8.GetBytes(key);
					options.SaveToken = true;

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(convertKeyToBytes),
						ValidateIssuer = true,
						ValidateAudience = true,
						RequireExpirationTime = true,
						ValidateLifetime = true,
						ValidAudience = audience,
						ValidIssuer = issuer,
						ClockSkew = TimeSpan.Zero
					};
				});

			return services;
		}
	}
}
