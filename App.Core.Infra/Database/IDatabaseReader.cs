using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    public interface IDatabaseReader
    {
        Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null);
        Task<TReturn> QuerySingleOrDefaultAsync<TReturn>(string sql, object param = null);
        Task<IEnumerable<TReturn>> ReadManyAsync<TReturn>(string sql, object param = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = null, CommandType commandType = CommandType.Text, bool buffered = true);
        Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = null, CommandType commandType = CommandType.Text, bool buffered = true);
        Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = null, CommandType commandType = CommandType.Text, bool buffered = true);
        Task<TReturn> QueryMultipleAsync<TReturn>(string sql, Func<SqlMapper.GridReader, TReturn> func, object param = null, CommandType commandType = CommandType.Text);
        Task<TReturn> ExecuteReaderAsync<TReturn>(string sql, Func<IDataReader, TReturn> func, object param = null, CommandType commandType = CommandType.Text);

        Task<TReturn> GetAsync<TReturn>(int id) where TReturn : class;
        Task<IEnumerable<TReturn>> GetAllAsync<TReturn>() where TReturn : class;
        Task<DataTable> GetDataTableSchemaFromQuery(string sql, object param = null, CommandType commandType = CommandType.Text);
        DataTable GetDataTableSchemaBy(string tableName);
        Task<DataTable> GetDataTableSchemaFromDataReaderBy(string tableName);
    }
}