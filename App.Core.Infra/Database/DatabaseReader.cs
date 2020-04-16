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

        public async Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<TReturn>(sql, param);
            }
        }

        public async Task<TReturn> QuerySingleOrDefaultAsync<TReturn>(string sql, object param = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<TReturn>(sql, param);
            }
        }

        public async Task<IEnumerable<TReturn>> ReadManyAsync<TReturn>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<TReturn>(sql, param, commandType: commandType);
            }
        }

        public async Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            }
        }

        public async Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            }
        }

        public async Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = null)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            }
        }

        public async Task<TReturn> QueryMultipleAsync<TReturn>(string sql, Func<SqlMapper.GridReader, TReturn> func, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var reader = await connection.QueryMultipleAsync(sql, param, commandType: commandType))
                {
                    return func(reader);
                }
            }
        }

        public async Task<TReturn> ExecuteReaderAsync<TReturn>(string sql, Func<IDataReader, TReturn> func, object param = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (IDataReader reader = await connection.ExecuteReaderAsync(sql, param, commandType: commandType))
                {
                    return func(reader);
                }
            }
        }
    }
}
