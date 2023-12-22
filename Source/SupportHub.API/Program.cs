using SupportHub.API.Configurations;
using SupportHub.API.Filters;
using SupportHub.Authentication.Application;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddApplicationInjection(configuration);

services.AddAuthorizationConfiguration(configuration);
services.AddAuthenticationConfiguration(configuration);
services.AddSwaggerConfiguration(configuration);
services.AddRoutingConfiguration(configuration);
services.AddCorsConfiguration(configuration);

services.AddScoped<ExceptionFilter>();
services.AddControllers(options => { options.Filters.AddService<ExceptionFilter>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	configuration
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
		.AddEnvironmentVariables();

	app.UseDeveloperExceptionPage();
}
else
{
	configuration
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
		.AddEnvironmentVariables();

	app.UseExceptionHandler("/error");
}

app.UseCors("Any");
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();