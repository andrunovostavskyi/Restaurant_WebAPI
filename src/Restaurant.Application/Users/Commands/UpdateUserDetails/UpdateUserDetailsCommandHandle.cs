using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails
{
    internal class UpdateUserDetailsCommandHandle(ILogger<UpdateUserDetailsCommandHandle> logger,
        IUserStore<User> userStore,
        IUserContext userContext)
        : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Update user: {userId}, with {@Request}", user!.Id, request);

            var userDb = await userStore.FindByIdAsync(user.Id, cancellationToken);
            if (userDb == null)
                throw new NotFoundException(nameof(User), user.Id);

            userDb.BirthDay = request.BirthDay;
            userDb.Nationality = request.Nationality;

            await userStore.UpdateAsync(userDb, cancellationToken);
        }
    }
}
