using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using TestAspCoreTuto.Tests.Test1.Requirements;

namespace TestAspCoreTuto.Bootstrapping.Authorizations
{
    public static class AutorizationExtension
    {
        public static void AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationHandler, ShouldBeAReaderAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ShouldHasADepartmentHandler>();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireClaim(ClaimTypes.Name)
                    .RequireClaim(ClaimTypes.Role)
                    .Build();

                //TODO +WORK
                //options.AddPolicy("defaultPolicy", policy => policy.RequireAuthenticatedUser());
                //TODO +WORK

                options.AddPolicy("AdminOnly",
                    policy => policy
                        .RequireRole("Admin")
                        .RequireClaim(ClaimTypes.Email));

                options.AddPolicy("ShouldBeADirectorInParis", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new ShouldBeADirectorRequirement());
                    policy.Requirements.Add(new ShouldHasADepartmentRequirement("PARIS"));
                });
            });
        }
    }
}
