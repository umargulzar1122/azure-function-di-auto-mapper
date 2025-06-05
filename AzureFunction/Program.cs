using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using HellowWord.AccountService;
using HellowWord.Validation;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();
// Add AutoMapper - Manual registration


// Register Dataverse Service Client
builder.Services.AddScoped<ServiceClient>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();

    var dataverseUrl = config["Dataverse:Url"];
    var clientId = config["Dataverse:ClientId"];
    var clientSecret = config["Dataverse:ClientSecret"];

    if (!string.IsNullOrEmpty(dataverseUrl) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
    {
        var connectionString2 = $"AuthType=ClientSecret;Url={dataverseUrl};ClientId={clientId};ClientSecret={clientSecret};";
        return new ServiceClient(connectionString2);
    }
    throw new InvalidOperationException("Dataverse connection configuration is missing");
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
    cfg.AllowNullCollections = true;
    cfg.AllowNullDestinationValues = true;
});

builder.Services.AddSingleton(mapperConfig);
builder.Services.AddSingleton<IMapper>(provider => new Mapper(mapperConfig));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IValidationContainer, ValidationContainer>();

builder.Build().Run();

