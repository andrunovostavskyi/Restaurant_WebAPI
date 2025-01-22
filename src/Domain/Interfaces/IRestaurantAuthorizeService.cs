using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Authorize;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantAuthorizeService
{
    bool Authorize(Restaurant restaurant, ResourceOperations resource);
}
