using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRoles
{
    internal class UnAssignUserRolesCommandHandle(ILogger<UnAssignUserRolesCommandHandle> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserContext userContext)
        : IRequestHandler<UnAssignUserRolesCommand>
    {
        public async Task Handle(UnAssignUserRolesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("UnAssign user roles: {@Request}", request);


            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(User),request.Email);

            var role = await roleManager.FindByNameAsync(request.Role)
                ?? throw new NotFoundException(nameof(IdentityRole),request.Role);

            await userManager.RemoveFromRoleAsync(user, role.ToString());
        }
    }
}
