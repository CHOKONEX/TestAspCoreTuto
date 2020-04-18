using System;

namespace App.Core.Infra.Migrations.Attributes
{
    public class EveryTimeMigrationAttribute : FluentMigrator.MigrationAttribute
    {
        public EveryTimeMigrationAttribute()
              : base(GetVersion())
        {
        }

        private static long GetVersion()
        {
            return long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
