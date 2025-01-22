using MediatR;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant
{
    public class DeleteDishForRestaurantCommand:IRequest
    {
        public int RestaurantId { get; set; }
        public DeleteDishForRestaurantCommand(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
