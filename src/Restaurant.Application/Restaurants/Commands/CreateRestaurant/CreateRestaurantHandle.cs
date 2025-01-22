using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositroty;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantHandle(IRestaurantRepository repository, 
    IMapper mapper,
    ILogger<CreateRestaurantHandle> loger,
    IUserContext usercontext) 
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = usercontext.GetCurrentUser();
        loger.LogInformation("{UserEmail} with {UserId} creating a new {@Restaurant}",user.Email,user.Id,request);

        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = user.Id;
        int id = await repository.Create(restaurant);
        return id;
    }

}
