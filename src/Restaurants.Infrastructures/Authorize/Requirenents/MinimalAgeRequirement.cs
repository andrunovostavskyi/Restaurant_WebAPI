using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructures.Authorize.Requirenents;

public class MinimalAgeRequirement: IAuthorizationRequirement
{
    public int MinimalAge { get; }
    public MinimalAgeRequirement(int minimalAge)
    {
        MinimalAge = minimalAge;
    }
}
