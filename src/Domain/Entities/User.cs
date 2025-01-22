using Microsoft.AspNetCore.Identity;
using System;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public DateTime? BirthDay { get; set; }
    public string? Nationality { get; set; }

    public List<Restaurant> Restaurants { get; set; } = default!;
}

