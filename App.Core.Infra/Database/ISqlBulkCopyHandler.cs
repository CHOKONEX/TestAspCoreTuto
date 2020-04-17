using App.Core.Infra.Models;
using System.Threading.Tasks;

namespace App.Core.Infra.Database
{
    public interface ISqlBulkCopyHandler
    {
        Task Execute(string sourceConnectionString, string targetConnectionString, string query, SqlBulkCopyHandlerParameters parameters);
    }
}