using CountPad.Application;
using CountPad.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.ReadFrom.Configuration(builder.Configuration)
				.CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);

// CACHE Service start
builder.Services.AddLazyCache();
builder.Services.AddOutputCache();
builder.Services.AddResponseCaching();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("RedisDb");
});
// CACHE Service end

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "CountPad", Version = "v1" });

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{{
					new OpenApiSecurityScheme()
					{
					   Reference=new OpenApiReference()
					   {
						   Id="Bearer",
						   Type=ReferenceType.SecurityScheme
					   }
					},
					new string[]{}
				}});
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(
		a => a.DisplayRequestDuration());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseOutputCache();
app.UseResponseCaching();

app.MapControllers();

app.Run();
