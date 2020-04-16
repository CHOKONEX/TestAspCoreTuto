using System.Data;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    public interface IDatabaseExecutor
    {
        Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text);
        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null);
    }
}