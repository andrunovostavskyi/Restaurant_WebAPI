using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Common;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    internal class GetAllRestaurantsQueryHandle(IRestaurantRepository repository,
        IMapper mapper,
        ILogger<GetAllRestaurantsQueryHandle> logger)
        : IRequestHandler<GetAllRestaurantQuery, ResultPage<RestaurantDTO>>
    {
        public async Task<ResultPage<RestaurantDTO>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get all restaurants");
            var (restaurants, totalRestaurant) = await repository.GetAllFilterAsync(
                request.FilterPhase!, 
                request.PageSize, 
                request.PageNumber,
                request.SortBy!,
                request.sortDirectory
                );

            var restaurantDTO = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            var resultPage = new ResultPage<RestaurantDTO>(restaurantDTO, request.PageNumber, request.PageSize, totalRestaurant);
            return (resultPage);
        }
    }
}
