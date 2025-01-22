using AutoMapper;
using FluentAssertions;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.PatchRestaurant;
using Restaurants.Domain.Entities;
using System.Configuration;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;
    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDTO_MapsCorrectly()
    {
        //arrange

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test",
            Category = "Ukraine",
            ContactEmail = "test@test",
            Description = "sasdasda",
            HasDelivery = true,
            ContactNumber = "121212123",
            Adress = new Adress
            {
                City = "TestCity",
                Street = "testStreet",
                PostalCode = "333-22"
            }
        };

        //act
        var result = _mapper.Map<RestaurantDTO>(restaurant);

        //assert
        result.Should().NotBeNull();
        result.Street.Should().Be(restaurant.Adress.Street);
        result.City.Should().Be(restaurant.Adress.City);
        result.PostalCode.Should().Be(restaurant.Adress.PostalCode);
        result.Name.Should().Be(restaurant.Name);
        result.HasDelivery.Should().Be(restaurant.HasDelivery);
        result.Id.Should().Be(restaurant.Id);
        result.Category.Should().Be(restaurant.Category);
    }

    [Fact]
    public void CreateMap_ForCreatecreatecreateRestaurantCommandTocreateRestaurant_MapsCorrectly()
    {
        //arrange 
        var createRestaurant = new CreateRestaurantCommand
        {
            Name = "Test",
            Category = "Ukraine",
            ContactEmail = "test@test",
            Description = "sasdasda",
            HasDelivery = true,
            ContactNumber = "121212123",
            City = "TestCity",
            Street = "testStreet",
            PostalCode = "333-22"
        };

        //act
        var result = _mapper.Map<Restaurant>(createRestaurant);

        //assert
        result.Should().NotBeNull();
        result.Adress.Street.Should().Be(createRestaurant.Street);
        result.Adress.City.Should().Be(createRestaurant.City);
        result.Adress.PostalCode.Should().Be(createRestaurant.PostalCode);
        result.Name.Should().Be(createRestaurant.Name);
        result.HasDelivery.Should().Be(createRestaurant.HasDelivery);
        result.Category.Should().Be(createRestaurant.Category);
        result.ContactEmail.Should().Be(createRestaurant.ContactEmail);
        result.ContactNumber.Should().Be(createRestaurant.ContactNumber);
        result.Description.Should().Be(createRestaurant.Description);
    }



    [Fact]
    public void CreateMap_ForPatchRestaurantCommandTocreateRestaurant_MapsCorrectly()
    {
        //arrange 
        var patchRestaurant = new PatchRestaurantCommand
        {
            Id = 1,
            Name = "Test",
            Description = "sasdasda",
            HasDelivery = true,
        };

        //act
        var result = _mapper.Map<Restaurant>(patchRestaurant);

        //assert
        result.Should().NotBeNull();
        result.Id.Should().Be(patchRestaurant.Id);
        result.Name.Should().Be(patchRestaurant.Name);
        result.HasDelivery.Should().Be(patchRestaurant.HasDelivery);
        result.Description.Should().Be(patchRestaurant.Description);
    }
}