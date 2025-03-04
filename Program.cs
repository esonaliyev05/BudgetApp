using BudgetApp.Auth.Controllers;
using BudgetApp.Auth.Repositories;
using BudgetApp.Auth.Repositories.Interface;
using BudgetApp.Auth.Services;
using BudgetApp.Auth.Services.Interface;
using BudgetApp.Data.Components;
using BudgetApp.Data.Context;
using BudgetApp.Data.Extentions;
using BudgetApp.Data.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddServiceConfiguration()
	.AddSwaggerService(builder.Configuration);

builder.Services.AddControllers()
	.AddApplicationPart(typeof(UserController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(logging=>logging.AddConsole());

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
	using var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		c.RoutePrefix = string.Empty;
	});
}
app.Use(async (context, next) =>
{
	context.Request.Path = context.Request.Path.Value.ToLower();
	await next.Invoke();
});
app.UseMiddleware<BugalterExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();