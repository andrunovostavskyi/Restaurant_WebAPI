using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Persistance;

namespace Restaurants.Infrastructures.Seeders
{
    internal class RestaurantSeed(RestaurantDbContext dbContext) : IRestaurantSeed
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurant();
                    dbContext.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if(!dbContext.UserRoles.Any())
                {
                    var userRoles = GetRoles();
                    dbContext.AddRange(userRoles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new(UserRoles.Admin){
                        NormalizedName = UserRoles.Admin.ToUpper()
                    },
                    new(UserRoles.Owner){
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new(UserRoles.User){
                        NormalizedName= UserRoles.User.ToUpper()
                    }
                ];

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurant()
        {
            List<Restaurant> restaurants = new List<Restaurant>
        {
        new ()
        {
            Id = 1,
            Name = "KFS",
            Description = "Famous fast food chain specializing in fried chicken.",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "contact@kfs.com",
            ContactNumber = "555-123-4567",
            Dishes = new List<Dish>
            {
                new ()
                {
                    Id = 1,
                    Name = "Chicken Bucket",
                    Description = "A bucket of crispy fried chicken.",
                    Price = 19.99M,
                    KiloCalories = 1200
                },
                new ()
                {
                    Id = 2,
                    Name = "Spicy Wings",
                    Description = "Spicy chicken wings with a side of dipping sauce.",
                    Price = 8.99M,
                    KiloCalories = 750
                }
            },
            Adress = new ()
            {
                City = "New York",
                Street = "5th Avenue",
                PostalCode = "10001"
            }
        },
        new ()
        {
            Id = 2,
            Name = "Pizza Town",
            Description = "Authentic Italian pizzas in your neighborhood.",
            Category = "Pizzeria",
            HasDelivery = true,
            ContactEmail = "hello@pizzatown.com",
            ContactNumber = "555-987-6543",
            Adress = new ()
            {
                City = "Los Angeles",
                Street = "Sunset Boulevard",
                PostalCode = "90027"
            },
            Dishes = new List<Dish>
            {
                new ()
                {
                    Id = 3,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza with fresh mozzarella and basil.",
                    Price = 12.50M,
                    KiloCalories = 850
                },
                new ()
                {
                    Id = 4,
                    Name = "Pepperoni Pizza",
                    Description = "Pizza with pepperoni slices and mozzarella cheese.",
                    Price = 13.50M,
                    KiloCalories = 900
                }
                }
                }
            };
            return restaurants;
        }
    }
}


