using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructures.Authorize;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant
{
    internal class DeleteDishHandle(ILogger<DeleteDishHandle> logger,
        IRestaurantRepository restaurantRep,
        IDishRepository dishRep,
        IRestaurantAuthorizeService authorize) 
        : IRequestHandler<DeleteDishForRestaurantCommand>
    {
        public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete dishes for restaurant with id = {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantRep.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!authorize.Authorize(restaurant, ResourceOperations.Delete))
                throw new NotAcces();

            if (restaurant.Dishes!.Any())
            {
                await dishRep.DeleteAll(restaurant.Dishes!);
            }
        }
    }
}
