using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    internal class GetDishesForRestaurantQueryHandle(
        ILogger<GetDishesForRestaurantQueryHandle> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRep)
        : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDTO>>
    {

        public async Task<IEnumerable<DishDTO>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get dishes for restaurnt with id = {RestaurantId}", request.RestaurantId);

            var restaurants = await restaurantRep.GetByIdAsync(request.RestaurantId);
            if (restaurants == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dishes = mapper.Map<IEnumerable<DishDTO>>(restaurants.Dishes);
            return dishes;
        }
    }
}
