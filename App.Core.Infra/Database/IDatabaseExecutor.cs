using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    public interface IDatabaseExecutor
    {
        Task<bool> DeleteAllAsync<TResult>() where TResult : class;
        Task<bool> DeleteAsync<TResult>(TResult entity) where TResult : class;
        Task<bool> DeleteAsync<TResult>(IEnumerable<TResult> entities) where TResult : class;
        Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text);
        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null);
        Task<int> ExecuteTransactionAsync(string sql, object param = null, CommandType commandType = CommandType.Text);
        Task<int> ExecuteTransactionScopeAsync(string sql, object param = null, CommandType commandType = CommandType.Text);
        Task<int> InsertAsync<TResult>(TResult entity) where TResult : class;
        Task<int> InsertAsync<TResult>(IEnumerable<TResult> entities) where TResult : class;
        Task<bool> UpdateAsync<TResult>(TResult entity) where TResult : class;
        Task<bool> UpdateAsync<TResult>(IEnumerable<TResult> entities) where TResult : class;
    }
}