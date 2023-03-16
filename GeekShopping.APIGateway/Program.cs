using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", options =>
	{
		options.Authority = "https://localhost:5001/";
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false
		};
	});
builder.Services.AddOcelot();
// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot();

app.Run();
