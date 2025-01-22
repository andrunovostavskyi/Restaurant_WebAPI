using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositroty;
using Xunit;

namespace Restaurants.Infrastructures.Authorize.Requirenents.Tests;

public class MinimalNumberRestaurantsRequirementsHandlerTests
{
    private readonly Mock<ILogger<MinimalNumberRestaurantsRequirementsHandler>> _loggerMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IRestaurantRepository> _restaurantRepMock;

    public MinimalNumberRestaurantsRequirementsHandlerTests()
    {
        _loggerMock = new Mock<ILogger<MinimalNumberRestaurantsRequirementsHandler>>();
        _userContextMock = new Mock<IUserContext>();
        _restaurantRepMock = new Mock<IRestaurantRepository>();
    }

    [Fact()]
    public async Task HandleRequirement_UserHaveCreateMultiplyRestaurants_ShouldSuccess()
    {
        //arrange
        var currentUser = new CurrentUser("1", "test@test",[], null, null);
        _userContextMock.Setup(s => s.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new Restaurant
            {
                OwnerId = currentUser.Id,
            },
            new Restaurant
            {
                OwnerId = currentUser.Id,
            },
            new Restaurant
            {
                OwnerId = "2",
            }
        };
        _restaurantRepMock.Setup(s => s.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new MinimalNumberRestaurantsRequirements(2);
        var context = new AuthorizationHandlerContext([requirement], null, null);
        var handler = new MinimalNumberRestaurantsRequirementsHandler(_loggerMock.Object,
                                                                    _userContextMock.Object, _restaurantRepMock.Object);

        //act
        await handler.HandleAsync(context);
        
        //assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirement_UserHaveCreateMultiplyRestaurants_ShouldFail()
    {
        var currentUser = new CurrentUser("1", "test@test", [], null, null);
        _userContextMock.Setup(s => s.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new Restaurant
            {
                OwnerId = currentUser.Id,
            },
            new Restaurant
            {
                OwnerId = "2",
            }
        };
        _restaurantRepMock.Setup(s => s.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new MinimalNumberRestaurantsRequirements(2);
        var context = new AuthorizationHandlerContext([requirement], null, null);
        var handler = new MinimalNumberRestaurantsRequirementsHandler(_loggerMock.Object,
                                                                    _userContextMock.Object, _restaurantRepMock.Object);

        //act
        await handler.HandleAsync(context);

        //assert
        context.HasFailed.Should().BeTrue();
    }
}