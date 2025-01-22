using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructures.Authorize.Requirenents;

public class MinimalNumberRestaurantsRequirements : IAuthorizationRequirement
{
    public int MinimalNumber { get; set; }
    public MinimalNumberRestaurantsRequirements(int minimalNumber)
    {
        MinimalNumber = minimalNumber;
    }
}
