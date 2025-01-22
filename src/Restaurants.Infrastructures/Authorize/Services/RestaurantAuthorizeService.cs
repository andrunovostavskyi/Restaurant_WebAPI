using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructures.Authorize.Services
{
    public class RestaurantAuthorizeService(ILogger<RestaurantAuthorizeService> logger,
        IUserContext userContext) : IRestaurantAuthorizeService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperations resource)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} with {UserId} authorize for restaurant {RestaurantName}",
                user!.Email, user.Id, restaurant.Name);

            if (resource == ResourceOperations.Read || resource == ResourceOperations.Create)
            {
                logger.LogInformation("Create/Read restaurant - successfully authorized");
                return true;
            }

            if (resource == ResourceOperations.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Delete restaurant - successfully authorized");
                return true;
            }

            if ((resource == ResourceOperations.Delete || resource == ResourceOperations.Update) && restaurant.OwnerId == user.Id)
            {
                logger.LogInformation("Delete/Update restaurant - successfully authorized");
                return true;
            }

            return false;
        }
    }
}
