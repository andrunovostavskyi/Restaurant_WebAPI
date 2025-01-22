using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Authorize;
using Restaurants.Infrastructures.Authorize.Requirenents;
using Restaurants.Infrastructures.Persistance;
using Restaurants.Infrastructures.Repository;
using Restaurants.Infrastructures.Seeders;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructures.Authorize.Services;

namespace Restaurants.Infrastructures.Extensions
{
    public static class ServiceExtensionCollection
    {
        public static void AddInfrastructures(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantConnectionString");
            service.AddDbContext<RestaurantDbContext>( options=>options.UseNpgsql(connectionString));

            service.AddScoped<IRestaurantSeed, RestaurantSeed>();
            service.AddScoped<IRestaurantRepository, RestaurantRepository>();
            service.AddScoped<IDishRepository, DishRepository>();
            service.AddScoped<IAuthorizationHandler,MinimalAgeRequirementHandler>();
            service.AddScoped<IAuthorizationHandler, MinimalNumberRestaurantsRequirementsHandler>();
            service.AddScoped<IRestaurantAuthorizeService,RestaurantAuthorizeService>();


            service.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();


            service.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimsType.Nationality))
                .AddPolicy(PolicyNames.HasRequiredAge, builder => builder.AddRequirements(new MinimalAgeRequirement(20)))
                .AddPolicy(PolicyNames.HasMinimalNumberRestaurants, builder => builder.AddRequirements(new MinimalNumberRestaurantsRequirements(2)));
        }
    }
}
