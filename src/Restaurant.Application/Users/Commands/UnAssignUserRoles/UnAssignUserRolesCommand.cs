using MediatR;

namespace Restaurants.Application.Users.Commands.UnAssignUserRoles;

public class UnAssignUserRolesCommand:IRequest
{
    public string Email { get; set; } = default!;
    public string Role { get; set;} = default!;
}

