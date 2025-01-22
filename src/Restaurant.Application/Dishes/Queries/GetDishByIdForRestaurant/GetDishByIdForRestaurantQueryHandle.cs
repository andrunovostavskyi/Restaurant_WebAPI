using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    internal class GetDishByIdForRestaurantQueryHandle(ILogger<GetDishByIdForRestaurantQueryHandle> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRep)
        : IRequestHandler<GetDishByIdForRestaurantQuery, DishDTO>
    {
        public async Task<DishDTO> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get dish for restaurant with id = {RestaurantID}", request.RestaurantId);

            var restaurant = await restaurantRep.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes!.FirstOrDefault(u => u.Id == request.DishId);
            if(dish ==null) 
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            DishDTO result = mapper.Map<DishDTO>(dish);
            return result;
        }
    }
}
