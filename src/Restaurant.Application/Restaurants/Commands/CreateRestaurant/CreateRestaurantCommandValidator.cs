using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> variable = ["Indian", "Ukraine", "English", "Germany"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            .Must(categ => variable.Contains(categ))
            .WithMessage("Category must contain Ukraine, Indian, English or Germany");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please write valid email adress");

        RuleFor(dto => dto.ContactNumber)
            .Matches(@"\d{1,14}$")
            .WithMessage("Invalid phone number format.(+1-9*)-***-****-***");


        RuleFor(dto => dto.PostalCode)
            .Matches(@"\d{3}-\d{2}")
            .WithMessage("Please write validd PostalCode(XXX-XX)");
    }
}
