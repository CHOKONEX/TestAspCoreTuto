using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestAspCoreTuto.Tests.Test1.Requirements
{
    public class ShouldBeADirectorRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAReaderAuthorizationHandler : AuthorizationHandler<ShouldBeADirectorRequirement>
    {
        private readonly IUserService _userService;

        public ShouldBeAReaderAuthorizationHandler(IUserService userService)
        {
            _userService = userService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeADirectorRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Email))
                return Task.CompletedTask;

            var emailAddress = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (_userService.GetAll().Any(x => x.Email == emailAddress))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
