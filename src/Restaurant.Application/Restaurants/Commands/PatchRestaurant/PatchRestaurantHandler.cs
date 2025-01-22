using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Infrastructures.Authorize;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.PatchRestaurant;
public class PatchRestaurantHandler(IRestaurantRepository repository,
    IMapper mapper,
    IRestaurantAuthorizeService authorize,
    ILogger<PatchRestaurantHandler> logger)
    : IRequestHandler<PatchRestaurantCommand>
{
    public async Task Handle(PatchRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update restaurant with id {RestaurantId} new fields{@Restaurant}", request.Id, request);

        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());


        if (!authorize.Authorize(restaurant, ResourceOperations.Update))
            throw new NotAcces();


        mapper.Map(request, restaurant);
        await repository.SaveChanges();
    }
}

