using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    public interface IDapperArchiveRepository
    {
        Task Archive_Table_Movies();
    }
}