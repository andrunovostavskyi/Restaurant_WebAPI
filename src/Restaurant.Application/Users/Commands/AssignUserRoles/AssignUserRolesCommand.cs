using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRoles
{
    public class AssignUserRolesCommand:IRequest
    {
        public string Email { get; set; } = default!;
        public string Role { get; set; } =default!;
    }
}
