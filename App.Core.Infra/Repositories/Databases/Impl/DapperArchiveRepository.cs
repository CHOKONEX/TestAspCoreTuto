using App.Core.Dto.Tests;
using App.Core.Infra.Database;
using App.Core.Infra.Extensions;
using App.Core.Infra.Models;
using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    [Singleton]
    public class DapperArchiveRepository : IDapperArchiveRepository
    {
        private readonly IDatabaseReader _databaseReader;
        private readonly ISqlFileQueryReader _sqlFileQueryReader;
        private readonly ISqlBulkCopyHandler _sqlBulkCopyHandler;
        private readonly IConfiguration _config;
        private readonly IDatabaseExecutor _databaseExecutor;
        private readonly ILogger<DapperArchiveRepository> _logger;

        public DapperArchiveRepository(IDatabaseReader databaseReader, IDatabaseExecutor databaseExecutor, ISqlFileQueryReader sqlFileQueryReader,
            ISqlBulkCopyHandler sqlBulkCopyHandler, IConfiguration config, ILogger<DapperArchiveRepository> logger)
        {
            _databaseReader = databaseReader ?? throw new ArgumentNullException(nameof(databaseReader)); ;
            _sqlFileQueryReader = sqlFileQueryReader ?? throw new ArgumentNullException(nameof(sqlFileQueryReader)); ;
            _sqlBulkCopyHandler = sqlBulkCopyHandler;
            _databaseExecutor = databaseExecutor;
            _config = config;
            _logger = logger;
        }

        public async Task Archive_Table_Movies()
        {
            string queryClean = "Delete from Movies_Archive";
            await _databaseExecutor.ExecuteAsync(queryClean);

            string sourceConnection = _config.GetConnectionString("MyConnectionString");
            //string targetConnection = _config.GetConnectionString("TargetConnectionString");
            string query = "select * from Movies";

            await CompareTables("Movies", "Movies_Archive");
            await CreateSql("Movies");

            SqlBulkCopyHandlerParameters parameters = new SqlBulkCopyHandlerParameters("Movies_Archive");
            parameters.EnableStreaming = true;
            parameters.BatchSize = 3;
            parameters.NotifyAfter = 3;
            await _sqlBulkCopyHandler.Execute(sourceConnection, sourceConnection, query, parameters);
        }

        private async Task CompareTables(string sourceTable, string targetTable)
        {
            DataTable sourceDataTable = _databaseReader.GetDataTableSchemaBy(sourceTable);
            DataTable targetDataTable = _databaseReader.GetDataTableSchemaBy(targetTable);

            DataTable sourceDataTable1 = await _databaseReader.GetDataTableSchemaFromDataReaderBy(sourceTable);
            DataTable targetDataTable1 = await _databaseReader.GetDataTableSchemaFromDataReaderBy(targetTable);

            _logger.LogDebug("sourceDataTable.SchemaEquals(targetDataTable) = {0}", sourceDataTable.SchemaEquals(targetDataTable));
            _logger.LogDebug("sourceDataTable.SchemaMatches(targetDataTable) = {0}", sourceDataTable.SchemaMatches(targetDataTable));

            _logger.LogDebug("sourceDataTable1.SchemaEquals(targetDataTable) = {0}", sourceDataTable1.SchemaEquals(targetDataTable1));
            _logger.LogDebug("sourceDataTable1.SchemaMatches(targetDataTable) = {0}", sourceDataTable1.SchemaMatches(targetDataTable1));

            _logger.LogDebug("====sourceDataTable");
            foreach (DataRow tableColumn in sourceDataTable.Rows)
            {
                foreach (DataColumn prop in sourceDataTable.Columns)
                {
                    //Console.WriteLine(prop.ColumnName + " = " + tableColumn[prop].ToString());
                }
            }
            _logger.LogDebug("====sourceDataTable1");
            foreach (DataRow tableColumn in sourceDataTable1.Rows)
            {
                foreach (DataColumn prop in sourceDataTable1.Columns)
                {
                    //Console.WriteLine(prop.ColumnName + " = " + tableColumn[prop].ToString());
                }
            }
        }

        private async Task CreateSql(string sourceTable)
        {
            DataTable sourceDataTable = await _databaseReader.GetDataTableSchemaFromDataReaderBy(sourceTable);
            string newTable = $"{sourceTable}_{DateTime.Now:yyyy_MM}";
            //string sql = SqlTableCreator.GetCreateFromDataTableSQL(newTable, sourceDataTable);

            int[] primaryKeysIndexes = SqlTableCreator.GetPrimaryKeysIndexes(sourceDataTable);
            string sql = SqlTableCreator.GetCreateSQL(newTable, sourceDataTable, primaryKeysIndexes);
            Console.WriteLine(sql);
        }
    }
}