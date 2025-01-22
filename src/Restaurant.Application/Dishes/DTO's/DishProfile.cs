using AutoMapper;
using restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.DTO_s
{
    public class DishProfile:Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>();
            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
