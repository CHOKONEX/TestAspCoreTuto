using BlazorCrud.Shared.Models;
using System.Threading.Tasks;

namespace BlazorCrud.Client.Services
{
    public interface IEmployeeServices
    {
        Task<Employee[]> GetList();
        Task<Employee> GetEmployeeDetails(int id);
        Task DeleteEmployee(int id);
        
    }
}