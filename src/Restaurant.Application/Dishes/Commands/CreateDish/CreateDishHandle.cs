using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructures.Authorize;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    internal class CreateDishHandle(ILogger<CreateDishHandle> logger,
        IRestaurantRepository restaurantRep,
        IDishRepository dishRep,
        IMapper mapper ): IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create new Dish with parametr {@DishProp}", request);
            var restaurant = await restaurantRep.GetByIdAsync(request.RestaurantId) 
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);
            return await dishRep.Create(dish);
        }
    }
}
