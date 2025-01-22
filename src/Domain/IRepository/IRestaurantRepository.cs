using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositroty;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant> GetByIdAsync(int id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task SaveChanges();
    Task<(IEnumerable<Restaurant>, int)> GetAllFilterAsync(string filterPhaser,int pageSize, int pageNumber,string sortBy, SortDirectory sortDirectory);
}

