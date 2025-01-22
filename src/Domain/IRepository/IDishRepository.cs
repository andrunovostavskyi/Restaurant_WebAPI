using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositroty
{
    public interface IDishRepository
    {
        Task<int> Create(Dish dish);
        Task DeleteAll(IEnumerable<Dish> dish);
    }
}
