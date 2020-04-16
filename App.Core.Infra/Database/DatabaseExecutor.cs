using Asp.Core.Attributes;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace App.Core.Infra.Database
{
    [Singleton]
    public class DatabaseExecutor : IDatabaseExecutor
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString;
        public DatabaseExecutor(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("MyConnectionString");
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, param, commandType: commandType);
            }
        }

        public async Task<int> ExecuteTransactionAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    int affectedRows = await connection.ExecuteAsync(sql, param, transaction: transaction, commandType: commandType);
                    transaction.Commit();
                    return affectedRows;
                }
            }
        }

        public async Task<int> ExecuteTransactionScopeAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            int affectedRows = 0;
            using (var transaction = new TransactionScope())
            {
                using (IDbConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    affectedRows = await connection.ExecuteAsync(sql, param, commandType: commandType);
                }

                transaction.Complete();
            }
            return affectedRows;
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.ExecuteScalarAsync<TResult>(sql, param);
            }
        }

        public async Task<int> InsertAsync<TResult>(TResult entity) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.InsertAsync(entity);
            }
        }

        public async Task<int> InsertAsync<TResult>(IEnumerable<TResult> entities) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.InsertAsync(entities);
            }
        }

        public async Task<bool> UpdateAsync<TResult>(TResult entity) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.UpdateAsync(entity);
            }
        }

        public async Task<bool> UpdateAsync<TResult>(IEnumerable<TResult> entities) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.UpdateAsync(entities);
            }
        }

        public async Task<bool> DeleteAsync<TResult>(TResult entity) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.DeleteAsync(entity);
            }
        }

        public async Task<bool> DeleteAsync<TResult>(IEnumerable<TResult> entities) where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.DeleteAsync(entities);
            }
        }

        public async Task<bool> DeleteAllAsync<TResult>() where TResult : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.DeleteAllAsync<TResult>();
            }
        }
    }
}
