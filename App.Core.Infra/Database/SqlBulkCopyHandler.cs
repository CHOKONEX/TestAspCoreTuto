using App.Core.Infra.Extensions;
using App.Core.Infra.Models;
using Asp.Core.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    [Singleton]
    public class SqlBulkCopyHandler : ISqlBulkCopyHandler
    {
        private readonly ILogger<SqlBulkCopyHandler> _logger;

        public SqlBulkCopyHandler(ILogger<SqlBulkCopyHandler> logger)
        {
            _logger = logger;
        }

        public async Task Execute(string sourceConnectionString, string targetConnectionString, string query,
            SqlBulkCopyHandlerParameters parameters)
        {
            using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
            using (SqlConnection targetConnection = new SqlConnection(targetConnectionString))
            {
                await Task.WhenAll(sourceConnection.OpenAsync(),
                                   targetConnection.OpenAsync());

                using (SqlCommand sourceCommand = new SqlCommand(query, sourceConnection))
                {
                    using (SqlDataReader reader = await sourceCommand.ExecuteReaderAsync())
                    {
                        using (SqlTransaction transaction = targetConnection.BeginTransaction())
                        {
                            try
                            {
                                int rowsCopied;
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(targetConnection, parameters.BulkCopyOptions, transaction))
                                {
                                    sqlBulkCopy.DestinationTableName = parameters.DestinationTableName;
                                    sqlBulkCopy.BatchSize = parameters.BatchSize;
                                    sqlBulkCopy.NotifyAfter = parameters.NotifyAfter;
                                    sqlBulkCopy.EnableStreaming = parameters.EnableStreaming;
                                    sqlBulkCopy.BulkCopyTimeout = parameters.BulkCopyTimeout;
                                    sqlBulkCopy.SqlRowsCopied += (sender, e) => SqlRowCopiedEvent(sender, e, parameters.DestinationTableName);

                                    await sqlBulkCopy.WriteToServerAsync(reader);
                                    rowsCopied = sqlBulkCopy.GetRowsCopied();
                                }
                                transaction.Commit();
                                _logger.LogDebug($"[{parameters.DestinationTableName}] Ended copy {rowsCopied} rows.");
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"[{parameters.DestinationTableName}] Exception: {ex}");
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
        }

        private void SqlRowCopiedEvent(object sender, SqlRowsCopiedEventArgs e, string destinationTableName)
        {
            _logger.LogDebug($"[{destinationTableName}] Copied {e.RowsCopied} so far...");
        }
    }
}
