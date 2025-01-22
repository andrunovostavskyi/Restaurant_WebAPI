using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositroty;
using Restaurants.Infrastructures.Authorize;
using System.Security.AccessControl;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.PatchRestaurant.Tests;

public class PatchRestaurantHandleTests
{
    private readonly Mock<ILogger<PatchRestaurantHandler>> _loggerMock;
    private readonly Mock<IRestaurantRepository> _restaurantRepMock;
    private readonly Mock<IRestaurantAuthorizeService> _restaurantAuthMock;
    private readonly Mock<IMapper> _mapperMock;


    public PatchRestaurantHandleTests()
    {
        _loggerMock = new Mock<ILogger<PatchRestaurantHandler>>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthMock = new Mock<IRestaurantAuthorizeService>();
        _restaurantRepMock = new Mock<IRestaurantRepository>();
    }
    [Fact()]
    public async Task PatchRestaurantHandle_ForValidRequest_UpdateRestaurant()
    {
        //arrange
        var restaurantId = 1;
        var command = new PatchRestaurantCommand
        {
            Id = restaurantId,
            Name = "name",
            Description = "Change on this",
            HasDelivery = true
        };

        var restaurant = new Restaurant
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
            HasDelivery = false,
        };
        _restaurantRepMock.Setup(s => s.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);

        _restaurantAuthMock.Setup(s => s.Authorize(restaurant, ResourceOperations.Update)).Returns(true);

        var patchRestaurantHandler = new PatchRestaurantHandler(_restaurantRepMock.Object, _mapperMock.Object,
                                                        _restaurantAuthMock.Object, _loggerMock.Object);

        //act
        await patchRestaurantHandler.Handle(command, CancellationToken.None);

        //assert
        _restaurantRepMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(r=>r.Map(command,restaurant), Times.Once);
    }

    [Fact]
    public async Task PatchRestaurantHandle_ForRestaurantDoNotExist_ThrowNotFoundExceptions()
    {
        //arrange
        var restaurantId = 1;
        var command = new PatchRestaurantCommand
        {
            Id = restaurantId
        };

        var restaurant = new Restaurant();
        _restaurantRepMock.Setup(s => s.GetByIdAsync(restaurantId))!.ReturnsAsync((Restaurant?)null);

        _restaurantAuthMock.Setup(s=>s.Authorize(restaurant,ResourceOperations.Update)).Returns(true);

        var patchRestaurantHandler = new PatchRestaurantHandler(_restaurantRepMock.Object, _mapperMock.Object,
                                                        _restaurantAuthMock.Object, _loggerMock.Object);
        //act
        Func<Task> act = async () => await patchRestaurantHandler.Handle(command, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Restaurant with id = {restaurantId} doesn't exist");
    }

    [Fact]
    public async Task PatchRestaurantHandle_ForAuthorizeDenied_ThrowNoAccessException()
    {
        //arrange
        var restaurantId = 1;
        var command = new PatchRestaurantCommand
        {
            Id = restaurantId
        };
        var restaurant = new Restaurant
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
            HasDelivery = false,
        };

        _restaurantRepMock.Setup(s => s.GetByIdAsync(restaurantId)).ReturnsAsync(restaurant);

        _restaurantAuthMock.Setup(s => s.Authorize(restaurant, ResourceOperations.Read)).Returns(false);

        var patchRestaurantHandler = new PatchRestaurantHandler(_restaurantRepMock.Object, _mapperMock.Object,
                                                        _restaurantAuthMock.Object, _loggerMock.Object);
        //act
        Func<Task> act = async () => await patchRestaurantHandler.Handle(command, CancellationToken.None);

        //assert
        await act.Should().ThrowAsync<NotAcces>().WithMessage("U don't have permission");
    }
}