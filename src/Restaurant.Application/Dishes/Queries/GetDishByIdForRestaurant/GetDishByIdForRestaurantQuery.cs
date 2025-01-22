using MediatR;
using restaurants.Application.Dishes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery:IRequest<DishDTO>
    {
        public int RestaurantId { get; set; }
        public int DishId { get; set; }
        public GetDishByIdForRestaurantQuery(int restaurantID, int dishId)
        {
            RestaurantId = restaurantID;
            DishId = dishId;
        }
    }
}
