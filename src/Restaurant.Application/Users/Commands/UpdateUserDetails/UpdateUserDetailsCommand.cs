using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateTime? BirthDay { get; set; }
        public string? Nationality { get; set; }
    }
}
