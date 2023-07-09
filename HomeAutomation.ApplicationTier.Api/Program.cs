using HomeAutomation.ApplicationTier.Api.Extensions;
using HomeAutomation.ApplicationTier.Entity;
using HomeAutomation.ApplicationTier.DataAccess;
using Microsoft.OpenApi.Models;
using HomeAutomation.ApplicationTier.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Build the initial configuration
var configuration = InitConfiguration(builder.Environment);

// Configure services
ConfigureServices(builder.Services);

// Create the host
var app = builder.Build();

// Configure the application
Configure(app, builder.Environment);

// Run the application
app.Run();

IConfiguration InitConfiguration(IWebHostEnvironment env)
{
    // Config the app to read values from appsettings based on the current environment value.
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    // Map AppSettings section in appsettings.json file value to AppSetting model
    configuration.GetSection("AppSettings").Get<AppSettings>(options => options.BindNonPublicProperties = true);

    return configuration;
}

void ConfigureServices(IServiceCollection services)
{
    services
        .AddDatabase()
        .AddServices()
        .AddCORS();

    services.AddMapster();
    services.AddControllers();
    services.AddApiVersioning();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1.0",
            new OpenApiInfo
            {
                Title = "ApplicationTier.API",
                Version = "v1.0"
            });
        c.OperationFilter<RemoveVersionParameterFilter>();
        c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
        c.EnableAnnotations();
    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        // Move swagger out of this if block if you want to use it in production        
    }

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "ApplicationTier.API v1.0"));

    // Auto redirect to HTTPS
    //app.UseHttpsRedirection();
    // Allow external access
    app.UseCors("CorsPolicy");
    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}