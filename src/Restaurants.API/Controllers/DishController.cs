using MediatR;
using Microsoft.AspNetCore.Mvc;
using restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("/api/restaurant/{restaurantId}/dishes")]
    public class DishController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            int dishId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdDishForRestaurant), new { restaurantId , dishId},null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetByIdDishForRestaurant([FromRoute] int restaurantId, [FromRoute]int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId));
            return NoContent();
        }

    }
}
