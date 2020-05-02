using App.Core.Infra.Migrations;
using FluentMigrator.Runner;
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
             //.AddLogging(lb => lb.AddFluentMigratorConsole())
             .AddFluentMigratorCore()
             .ConfigureRunner(
                 builder => builder
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    //.WithMigrationsIn(typeof(Migrator).Assembly)
                    .ScanIn(typeof(MigratorInjetionModule).Assembly).For.Migrations())
             .BuildServiceProvider(false);

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
