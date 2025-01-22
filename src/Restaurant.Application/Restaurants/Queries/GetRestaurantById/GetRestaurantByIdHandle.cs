using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    internal class GetRestaurantByIdHandle(IRestaurantRepository repository,
        IMapper mapper,
        ILogger<GetRestaurantByIdHandle> logger)
        : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get restaurants with id = {RestaurantId}", request.Id);

            var restaurant = await repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
            var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDTO;
        }
    }
}
