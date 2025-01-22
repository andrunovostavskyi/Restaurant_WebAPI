using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositroty;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantHandleTests
{
    [Fact()]
    public async Task Handle_ForCreateRestaurantCommand_ReturnCreateRestaurantId()
    {
        //arrange
        var mapper = new Mock<IMapper>();
        var createRestaurant = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapper.Setup(s => s.Map<Restaurant>(createRestaurant)).Returns(restaurant);

        var logger = new Mock<ILogger<CreateRestaurantHandle>>();

        var restaurantRepository = new Mock<IRestaurantRepository>();
        restaurantRepository.Setup(s => s.Create(It.IsAny<Restaurant>())).ReturnsAsync(1);

        var userContext = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id","test@test",[UserRoles.User],null,null);
        userContext.Setup(s => s.GetCurrentUser()).Returns(currentUser);


        var commandHandler = new CreateRestaurantHandle(restaurantRepository.Object, mapper.Object, logger.Object,userContext.Object);

        //act
        var result = await commandHandler.Handle(createRestaurant,CancellationToken.None);

        //assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be(currentUser.Id);
        restaurantRepository.Verify(r => r.Create(restaurant), Times.Once);

    }
}