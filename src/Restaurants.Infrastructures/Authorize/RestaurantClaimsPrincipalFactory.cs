using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructures.Authorize;

public class RestaurantClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    public RestaurantClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
    : base(userManager, roleManager, options)
    {
    }


    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimsType.Nationality, user.Nationality));
        }

        if (user.BirthDay != null)
        {
            id.AddClaim(new Claim(AppClaimsType.DateOfBirth, user.BirthDay.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }

}

