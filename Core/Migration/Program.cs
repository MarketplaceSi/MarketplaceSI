using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

Console.WriteLine("Configuration migration startup.");
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

Console.WriteLine("> Configuration migration for environment: {0}.", environmentName);

try
{
    Console.WriteLine("> Start building configurations file for env {0}.", environmentName);
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
    var configuration = builder.Build();

    Console.WriteLine("> ConnectionString to be used: {0}", configuration.GetConnectionString("Default"));

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

    using (var context = new AppDbContext(optionsBuilder.Options))
    {
        var pendingMutations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMutations.Any())
        {
            Console.WriteLine("> Founded {0} pending migrations.", pendingMutations.Count());
            await context.Database.MigrateAsync();
            Console.WriteLine("> Database updated.");
        }
    }

    Console.WriteLine("> Successe database update.");
}
catch (Exception ex)
{

    Console.WriteLine("> Failed to start Migration: {0}", ex.Message);
}