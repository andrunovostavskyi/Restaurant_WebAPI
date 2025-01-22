using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructures.Persistance;
using Restaurants.Domain.Constants;
using System.Globalization;
using System.Linq.Expressions;

namespace Restaurants.Infrastructures.Repository;
internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        dbContext.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(i => i.Dishes).ToListAsync();

        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllFilterAsync(string filterPhaser, int pageSize, int pageNumber, string sortBy, SortDirectory sortDirectory)
    {
        var filterPhaseLower = filterPhaser?.ToLower();

        var baseQuery = dbContext.Restaurants
           .Include(i => i.Dishes)
           .Where(u => filterPhaser == null || (u.Name.ToLower().Contains(filterPhaseLower!)
           || u.Description.ToLower().Contains(filterPhaseLower!)));

        var totalRestaurant = baseQuery.Count();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name), r=>r.Name },
                {nameof(Restaurant.Description), r=>r.Description },
                {nameof(Restaurant.Category), r=>r.Category }
            };
            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirectory == SortDirectory.Ascending ? baseQuery.OrderBy(selectedColumn) :
                                                                   baseQuery.OrderByDescending(selectedColumn);
        }

        var rest = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

        return (rest, totalRestaurant);
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.Include(i => i.Dishes).FirstOrDefaultAsync(u => u.Id == id);
        return restaurant!;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

    public async Task<Restaurant> Update(int id, string name, string description, bool hasDelivery)
    {
        var restaurant = await GetByIdAsync(id);
        if (restaurant == null)
            return restaurant!;
        restaurant.Name = name;
        restaurant.Description = description;
        restaurant.HasDelivery = hasDelivery;
        dbContext.Update(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant;

    }
}
