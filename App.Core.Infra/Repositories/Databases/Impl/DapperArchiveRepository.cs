using App.Core.Infra.Database;
using App.Core.Infra.Extensions;
using App.Core.Infra.Models;
using App.Core.Infra.SqlResourcesReader;
using Asp.Core.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
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
            string sourceTable = "Movies";
            string targetTable = $"Movies_Archive_{DateTime.Now:yyyy_MM}";

            //string queryClean = $"Delete from {targetTable}";
            //await _databaseExecutor.ExecuteAsync(queryClean);   

            string sourceConnection = _config.GetConnectionString("MyConnectionString");
            //string targetConnection = _config.GetConnectionString("TargetConnectionString");
            string query = $"select * from {sourceTable}";

            //await CreateSql("Movies");
            await CreateTableSchema(sourceTable, targetTable);
            await CompareTables(sourceTable, $"Movies_Archive_{DateTime.Now:yyyy_MM}");

            SqlBulkCopyHandlerParameters parameters = new SqlBulkCopyHandlerParameters(targetTable);
            parameters.EnableStreaming = true;
            parameters.BatchSize = 3;
            parameters.NotifyAfter = 3;
            await _sqlBulkCopyHandler.Execute(sourceConnection, sourceConnection, query, parameters);
        }

        private async Task CreateTableSchema(string sourceTable, string targetTable)
        {
            await DropTableIfExist(targetTable);

            string queryCreateTable = _sqlFileQueryReader.GetQuery("exec_spc_common_create_CreateTableFromExistingTable.sql");
            object paramsCreateTable = new { SourceTableName = sourceTable, TargetTableName = targetTable, AddDropIfItExists = false };
            await _databaseExecutor.ExecuteAsync(queryCreateTable, paramsCreateTable);
        }

        private async Task DropTableIfExist(string targetTable)
        {
            string queryCheckTableExist = _sqlFileQueryReader.GetQuery("common_check_table_if_exist.sql");
            object paramsCheckTableExist = new { TableName = targetTable };
            bool? isExist = await _databaseReader.QueryFirstOrDefaultAsync<bool?>(queryCheckTableExist, paramsCheckTableExist);
            if (isExist.HasValue && isExist.Value)
            {
                string queryDropTableExist = _sqlFileQueryReader.GetQuery("common_drop_table_if_exist.sql");
                object paramsDropTableExist = new { TableName = targetTable };
                bool onSuccess = await _databaseExecutor.ExecuteScalarAsync<bool>(queryDropTableExist, paramsDropTableExist);
                if (onSuccess)
                {
                    _logger.LogDebug($"{targetTable} was dropped successfully");
                }
                else
                {
                    _logger.LogError($"{targetTable} was not dropped");
                    throw new Exception($"{targetTable} was not dropped");
                }
            }
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

            int[] primaryKeysIndexes = SqlTableCreator.GetPrimaryKeysIndexes(sourceDataTable);
            string sql = SqlTableCreator.GetCreateSQL(newTable, sourceDataTable, primaryKeysIndexes);
            Console.WriteLine(sql);
        }
    }
}