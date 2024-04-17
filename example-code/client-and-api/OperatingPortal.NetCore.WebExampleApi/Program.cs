using Microsoft.OpenApi.Models;
using OperatingPortal.NetCore.WebExampleApi;
using OperatingPortal.NetCore.WebExampleApi.Code;
using VPBase.Client.Code.Settings;

var builder = WebApplication.CreateBuilder(args);

// Inform client of settings to be able to log etc. using the log4net rest appender
var clientAppSettings = builder.Configuration.GetSection("AppSettings").Get<ClientAppSettings>();
ClientAppSettingsHelper.ApplyAppSettings(clientAppSettings);

// Add logging
builder.Logging
    .ClearProviders()
    .AddDebug()
    .AddConsole()
    .AddLog4Net(log4NetConfigFile: "log4net.config");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register the Swagger generator, defining the Swagger documents
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(Definitions.ModuleGroupName, SwaggerHelper.GetOpenApiModuleInfo(Definitions.ModuleName));

    options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });

});

var app = builder.Build();

// Simple log 
app.Logger.LogInformation("Example log information");

// Advanced log and hearbeat example 
LogHeartbeatExample.Execute(app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(options =>
    {
        // Operating Portal
        options.SwaggerEndpoint("/swagger/" + Definitions.ModuleGroupName + "/swagger.json", "VPBase5 " + Definitions.ModuleName + " API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


