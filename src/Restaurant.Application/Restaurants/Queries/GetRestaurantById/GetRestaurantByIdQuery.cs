using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery:IRequest<RestaurantDTO>
    {
        public int Id { get; set; }
        public GetRestaurantByIdQuery(int id)
        {
            Id = id;
        }
    }
}
