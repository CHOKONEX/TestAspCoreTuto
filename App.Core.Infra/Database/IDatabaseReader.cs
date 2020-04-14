using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    public interface IDatabaseReader
    {
        Task<TReturn> ReadAsync<TReturn>(string sql, object param = null);
        Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn, object param = null);
        Task<IEnumerable<TReturn>> ReadOneToManyAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn, object param = null);
    }
}