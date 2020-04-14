using Asp.Core.Attributes;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    [Singleton]
    public class DatabaseReader : IDatabaseReader
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString;
        public DatabaseReader(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("MyConnectionString");
        }

        public async Task<TReturn> ReadAsync<TReturn>(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return (await connection.QueryAsync<TReturn>(sql, param)).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            }
        }

        public async Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            }
        }
    }
}
