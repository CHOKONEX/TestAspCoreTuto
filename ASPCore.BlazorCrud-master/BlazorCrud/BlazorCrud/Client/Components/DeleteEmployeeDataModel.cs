using System;
using System.Threading.Tasks;
using BlazorCrud.Client.Services;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCrud.Client.Components
{
    public class DeleteEmployeeDataModel : ComponentBase
    {
        [Inject]
        protected IEmployeeServices employeeServices { get; set; }
        [Inject]
        protected NavigationManager urlNavigationManager { get; set; }

        [Parameter]
        public int empID { get; set; }

        protected Employee emp { get; set; } = new Employee();

        

        protected override async Task OnInitializedAsync()
        {
            emp = await employeeServices.GetEmployeeDetails(empID);
        }

        protected async Task Delete()
        {
            await employeeServices.DeleteEmployee(Convert.ToInt32(empID));
            urlNavigationManager.NavigateTo("/fetchemployee");
        }

        protected void Cancel()
        {
            urlNavigationManager.NavigateTo("/fetchemployee");
        }
    }
}
