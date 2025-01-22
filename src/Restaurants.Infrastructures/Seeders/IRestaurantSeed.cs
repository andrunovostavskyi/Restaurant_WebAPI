using Restaurants.Infrastructures.Persistance;

namespace Restaurants.Infrastructures.Seeders
{
    public interface IRestaurantSeed
    {
        Task Seed();
    }
}