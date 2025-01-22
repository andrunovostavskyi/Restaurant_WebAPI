using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

internal class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(t => t.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilocalories must be equal or greater than 0");

        RuleFor(t => t.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilocalories must be equal or greater than 0");
    }
}
