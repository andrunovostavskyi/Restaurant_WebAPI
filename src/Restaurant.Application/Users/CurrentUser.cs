using Restaurants.Domain.Entities;

namespace Restaurants.Application.Users;

public record class CurrentUser(string Id, 
    string Email, 
    IEnumerable<string> Roles, 
    string? Nationality, 
    DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
