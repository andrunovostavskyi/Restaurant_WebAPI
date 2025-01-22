using FluentValidation;
using FluentValidation.Validators;

namespace Restaurants.Application.Restaurants.Commands.PatchRestaurant
{
    public class PathRestorauntCommandValidator:AbstractValidator<PatchRestaurantCommand>
    {
        public PathRestorauntCommandValidator()
        {
            RuleFor(com => com.Name)
                .Length(3, 100);

            RuleFor(com => com.Description)
                .NotEmpty();
        }
    }
}
