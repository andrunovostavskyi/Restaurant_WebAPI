using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructures.Authorize.Requirenents;

internal class MinimalAgeRequirementHandler(ILogger<MinimalAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimalAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimalAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser();
        if(user is null)
        {
            logger.LogWarning("User is null");
            context.Fail(); 
            return Task.CompletedTask;
        }

        if(user!.DateOfBirth == null)
        {
            logger.LogWarning("Birthday is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if(user.DateOfBirth.Value.AddYears(requirement.MinimalAge)<=DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorize succeed");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorize is not succeed");
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
