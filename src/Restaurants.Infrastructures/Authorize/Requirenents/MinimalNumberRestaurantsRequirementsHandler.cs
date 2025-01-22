using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositroty;

namespace Restaurants.Infrastructures.Authorize.Requirenents;

public class MinimalNumberRestaurantsRequirementsHandler(ILogger<MinimalNumberRestaurantsRequirementsHandler> logger,
    IUserContext userContext,
    IRestaurantRepository restaurantRep) : AuthorizationHandler<MinimalNumberRestaurantsRequirements>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimalNumberRestaurantsRequirements requirement)
    {
        var user = userContext.GetCurrentUser();
        if(user == null)
        {
            logger.LogWarning("User is null");
            context.Fail();
            return;
        }

        logger.LogInformation("{UserEmail} {UserId} -- authorize with minimal restaurants", user!.Email, user.Id);

        var restaurants = await restaurantRep.GetAllAsync();
        int count = restaurants.Count(u => u.OwnerId == user!.Id);

        if (count < requirement.MinimalNumber)
        {
            logger.LogWarning("User has enought restaurants");
            context.Fail(); 
        }
        else
        {
            logger.LogWarning("Authorize succesed");
            context.Succeed(requirement);
        }
    }
}
