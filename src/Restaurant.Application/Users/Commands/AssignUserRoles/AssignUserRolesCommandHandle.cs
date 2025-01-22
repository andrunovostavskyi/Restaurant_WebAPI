using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Users.Commands.AssignUserRoles
{
    internal class AssignUserRolesCommandHandle(ILogger<AssignUserRolesCommandHandle> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        : IRequestHandler<AssignUserRolesCommand>
    {
        public async Task Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assign user roles: {@Request}", request);
            
            var user = await userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(User), request.Email);

            var roles = await roleManager.FindByNameAsync(request.Role)
                ?? throw new NotFoundException(nameof(IdentityRole), request.Role);

            await userManager.AddToRoleAsync(user, roles.ToString()!);
        }
    }
}
