using Customer.SupportHub.API.Configurations;
using Customer.SupportHub.API.Filters;
using Customer.SupportHub.Application;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddApplicationInjection(configuration);
services.AddAuthenticationConfiguration(configuration);
services.AddSwaggerConfiguration();
services.AddRoutingConfiguration();
services.AddCorsConfiguration();

services.AddScoped<ExceptionFilter>();
services.AddControllers(options => { options.Filters.AddService<ExceptionFilter>(); });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	configuration.AddUserSecrets<Program>();
}
else
{
	app.UseHsts();
	app.UseExceptionHandler("/error");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();