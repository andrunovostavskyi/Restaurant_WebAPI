using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRoles;
using Restaurants.Application.Users.Commands.UnAssignUserRoles;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("/api/identity")]
    [Authorize]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUser(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("assign")]
        [Authorize(Roles =UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRoles(AssignUserRolesCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("unAssign")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnAssignUserRoles(UnAssignUserRolesCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            return Ok(claims);
        }

    }
}
