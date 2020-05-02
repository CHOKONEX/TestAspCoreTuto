using App.Core.Infra.Migrations.Attributes;
using App.Core.Infra.SqlResourcesReader;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Core.Infra.Migrations.Schema.Procedures
{
    [EveryTimeMigration]
    public class PreDeploymentScriptsExecutor : Migration
    {
        private readonly ISqlFileQueryReader _sqlFileQueryReader;

        public PreDeploymentScriptsExecutor(ISqlFileQueryReader sqlFileQueryReader)
        {
            _sqlFileQueryReader = sqlFileQueryReader;
        }

        public override void Up()
        {
            IEnumerable<string> scripts = typeof(InfraInjectionModule).Assembly
                .GetManifestResourceNames()
                .Where(x =>
                    x.Contains("Deployment", StringComparison.CurrentCultureIgnoreCase) &&
                    x.EndsWith(".sql")
                 ).ToList();

            foreach (string script in scripts)
            {
                string query = _sqlFileQueryReader.GetQuery(script);
                Execute.Sql(query);
                Console.WriteLine($"{script} is created");
            }
        }

        public override void Down()
        {
            // Nothing to do here
        }
    }

}
