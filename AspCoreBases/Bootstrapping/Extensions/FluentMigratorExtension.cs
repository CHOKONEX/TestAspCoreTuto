using System;
using System.Linq;
using App.Core.Infra.Migrations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestAspCoreTuto.Bootstrapping.Extensions
{
    public static class FluentMigratorExtension
    {
        public static void AddMigratorExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("MyConnectionString");

            ServiceProvider serviceProvider = services
             // Logging is the replacement for the old IAnnouncer
             .AddLogging(lb => lb.AddFluentMigratorConsole())
             // Registration of all FluentMigrator-specific services
             .AddFluentMigratorCore()
             // Configure the runner
             .ConfigureRunner(
                 builder => builder
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    //.WithMigrationsIn(typeof(Migrator).Assembly)
                    .ScanIn(typeof(MigratorInjetionModule).Assembly).For.Migrations())
             .BuildServiceProvider();

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
