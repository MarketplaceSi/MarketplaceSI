using HotChocolate.AspNetCore.Voyager;
using HotChocolate.AspNetCore;
using Kernel.Extensions;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Extensions;
using Microsoft.Extensions.Options;
using Serilog;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .Build();


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration, ConfigurationLoggerConfigurationExtensions.DefaultSectionName)
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host
        .AddConfigurations()
        .UseSerilog();

    builder.Services
        .ConfigureApplicationServices(builder.Configuration, builder.Environment)
        .AddMemoryCache()
        .ConfigureGraphQl();

    var app = builder.Build();
    var setting = app.Services.GetRequiredService<IOptions<SecuritySettings>>();
    var cors = app.Services.GetRequiredService<IOptions<CorsSettings>>();

    app
        .UseSerilogRequestLogging()
        .ConfigureRequestPipeline(app.Environment, setting.Value, cors.Value)
        .UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL()
                .WithOptions(new GraphQLServerOptions
                {
                    Tool =
                    {
                        //GaTrackingId = "google-analytics-id",
                        GaTrackingId = "G-2Y04SFDV8F",
                    }
                });

            app.UseVoyager("/graphql", "/graphql-voyager");

            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/graphql", true);
                return Task.CompletedTask;
            });
        });

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}