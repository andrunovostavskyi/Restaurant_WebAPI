using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Persistance;

namespace Restaurants.Infrastructures.Repository
{
    internal class DishRepository(RestaurantDbContext context) : IDishRepository
    {
        public async Task<int> Create(Dish dish)
        {
            context.Dishes.Add(dish);
            await context.SaveChangesAsync();
            return dish.Id;
        }

        public async Task DeleteAll(IEnumerable<Dish> dish)
        {
            context.Dishes.RemoveRange(dish);
            await context.SaveChangesAsync();
        }
    }
}
