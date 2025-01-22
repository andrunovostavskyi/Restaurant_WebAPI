using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.PatchRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs;

public class RestaurantProfile:Profile
{
    public RestaurantProfile()
    {
        CreateMap<PatchRestaurantCommand, Restaurant>();

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d=>d.Adress, opt=>opt.MapFrom(exp=> new Adress
            {
                City = exp.City,
                PostalCode = exp.PostalCode,
                Street = exp.Street
            } ));

        CreateMap<Restaurant, RestaurantDTO>()
            .ForMember(d=>d.Street, opt=>opt.MapFrom(exp=>exp.Adress!.Street))
            .ForMember(d => d.City, opt => opt.MapFrom(exp => exp.Adress!.City))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(exp => exp.Adress!.PostalCode));
    }
}
