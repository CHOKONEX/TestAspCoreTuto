using App.Core.Dto.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Infra.Repositories.Databases
{
    public interface ICustomerDapperTypeRepository
    {
        Task<IEnumerable<CustomerModel>> GetAllCustomers();
    }
}