using System;
using System.Data.SqlClient;

namespace App.Core.Infra.Models
{
    /*
     * SqlBulkCopyOptions options = SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.TableLock;
     * 
     * When EnableStreaming is true, SqlBulkCopy reads from an IDataReader object using SequentialAccess, optimizing memory usage by using the IDataReader streaming capabilities.
     * When it's set to false, the SqlBulkCopy class loads all the data returned by the IDataReader object into memory before sending it to SQL Server or SQL Azure. 
     */

    public class SqlBulkCopyHandlerParameters
    {
        public SqlBulkCopyHandlerParameters(string destinationTableName)
        {
            DestinationTableName = destinationTableName ?? throw new ArgumentNullException(nameof(destinationTableName));
        }

        public bool EnableStreaming { get; set; } = true;
        public string DestinationTableName { get; set; }
        public int BulkCopyTimeout { get; set; }
        public int BatchSize { get; set; } = 5000;
        public int NotifyAfter { get; set; } = 50000;
        public SqlBulkCopyOptions BulkCopyOptions { get; set; }
    }
}
