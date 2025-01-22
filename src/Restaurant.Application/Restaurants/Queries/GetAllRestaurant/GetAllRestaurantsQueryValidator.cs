using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantsQueryValidator:AbstractValidator<GetAllRestaurantQuery>
{
    private readonly int[] allowPageSize = [5, 10, 20, 30];
    private readonly string[] allowSortWord = [nameof(RestaurantDTO.Name),
                                               nameof(RestaurantDTO.Description),
                                               nameof(RestaurantDTO.Category)];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSize.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSize)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowSortWord.Contains(value))
            .When(q=>q.SortBy != null)
            .WithMessage($"Sort must be optimal or include [{string.Join(",", allowSortWord)}]");
    }
}
