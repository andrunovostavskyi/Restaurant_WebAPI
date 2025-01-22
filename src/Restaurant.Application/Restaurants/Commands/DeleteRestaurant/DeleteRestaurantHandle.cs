using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositroty;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructures.Authorize;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleterestaurantHandle(IRestaurantRepository repository,
        ILogger<DeleterestaurantHandle> logger,
        IRestaurantAuthorizeService authorize) 
        : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete restaurant with {RestaurantId}", request.Id);
            var restaurant = await repository.GetByIdAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

            if (!authorize.Authorize(restaurant, ResourceOperations.Delete))
                throw new NotAcces();

            await repository.Delete(restaurant);
        }
    }
}
