using Asp.Core.Attributes;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        public async Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.ExecuteScalarAsync<TResult>(sql, param);
            }
        }
    }
}
