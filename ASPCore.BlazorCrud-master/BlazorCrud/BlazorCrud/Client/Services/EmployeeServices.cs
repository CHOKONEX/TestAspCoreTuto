using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorCrud.Client.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly HttpClient _http;

        public EmployeeServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<Employee[]> GetList()
        {
            return await _http.GetJsonAsync<Employee[]>("/api/Employee/Index");
        }

        public async Task<Employee> GetEmployeeDetails(int id)
        {
            return await _http.GetJsonAsync<Employee>("/api/Employee/Details/" + id);
        }

        public async Task DeleteEmployee(int id)
        {
            await _http.DeleteAsync("api/Employee/Delete/" + id);
        }
    }
}
