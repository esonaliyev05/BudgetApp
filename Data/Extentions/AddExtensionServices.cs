using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Auth.Repositories;
using BudgetApp.Auth.Services.Interface;
using BudgetApp.Auth.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BudgetApp.Data.Components;
using BudgetApp.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Data.Extentions;

public static class AddExtensionServices
{
	public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
	{
		services.AddScoped<ITokenRepository, TokenRepository>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IUserRepository, UserRepository>();
		return services;
	}

	public static void AddSwaggerService(this IServiceCollection services,IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("Jwt");
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings["Issuer"],
					ValidAudience = jwtSettings["Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
			});
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "BugalteryAPI",
				Version = "v1",
				Description = "API for managing budget and transactions",
				Contact = new OpenApiContact
				{
					Name = "Your Name",
					Email = "your-email@example.com"
				}
			});
			//builder.Services.AddSwaggerGen(c =>
			//{
			//	c.SwaggerDoc("v1", new OpenApiInfo { Title = "BugalteryAPI", Version = "v1" });
			//	c.IgnoreObsoleteProperties();
			//});

			// JWT uchun Bearer token qo‘shish
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter JWT token with 'Bearer ' prefix (e.g., Bearer your_token)",
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[]{}
					}
				});
		});
		services.AddIdentity<User, IdentityRole<Guid>>(options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequiredLength = 8;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = true;
			options.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();
	}
}
