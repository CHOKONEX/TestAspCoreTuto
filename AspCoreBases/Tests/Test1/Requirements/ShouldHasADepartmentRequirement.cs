using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TestAspCoreTuto.Tests.Test1.Requirements
{
    public class ShouldHasADepartmentRequirement : IAuthorizationRequirement
    {
        public string Department { get; }

        public ShouldHasADepartmentRequirement(string department)
        {
            Department = department;
        }
    }

    public class ShouldHasADepartmentHandler : AuthorizationHandler<ShouldHasADepartmentRequirement>
    {
        private readonly IUserService _userService;

        public ShouldHasADepartmentHandler(IUserService userService)
        {
            _userService = userService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldHasADepartmentRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Email))
                return Task.CompletedTask;

            var emailAddress = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = _userService.GetAll().FirstOrDefault(x => x.Email == emailAddress);
            if (user != null && user.Department == requirement.Department)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
