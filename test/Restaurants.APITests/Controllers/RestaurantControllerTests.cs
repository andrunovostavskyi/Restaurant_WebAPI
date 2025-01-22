using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.APITests.Controllers;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositroty;
using System.Net.Http.Json;
using Xunit;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantControllerTests:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantRepository> _restaurantRepMock = new();

    public RestaurantControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(service =>
            {
                service.AddSingleton<IPolicyEvaluator, FakePolisyEvaluator>();
                service.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository), _=> _restaurantRepMock.Object));
            });
        });
    }

    [Fact()]
    public async Task GetAll_ForValidRequest_ShouldReturn200Ok()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var res = await client.GetAsync("/api/Restaurant?PageNumber=1&PageSize=5");

        //asert
        res.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact()]
    public async Task GetAll_ForNotValidRequest_ShouldReturn400BadRequest()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var res = await client.GetAsync("/api/Restaurant");

        //asert
        res.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }


    [Fact()]
    public async Task GetByID_ForNotExistRestaurant_ShouldReturn404NotFound()
    {
        //arrange
        int id = 1121;
        _restaurantRepMock.Setup(s => s.GetByIdAsync(id))!.ReturnsAsync((Restaurant?)null);
        var client = _factory.CreateClient();
        
        //act
        var res = await client.GetAsync($"/api/Restaurant/{id}");

        //asert
        res.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }



    [Fact()]
    public async Task GetByID_ForExistRestaurant_ShouldReturn200Ok()
    {
        //arrange
        int id = 1121;
        var rest = new Restaurant
        {
            Id = id,
            Name = "test",
            Description = "testing"
        };
        _restaurantRepMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(rest);
        var client = _factory.CreateClient();

        //act
        var responce = await client.GetAsync($"/api/Restaurant/{id}");
        var restaurantDTO = responce.Content.ReadFromJsonAsync<Restaurant>();

        //asert
        responce.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        restaurantDTO.Should().NotBeNull();
        restaurantDTO.Result.Name.Should().Be("test");
        restaurantDTO.Result.Description.Should().Be("testing");
    }

}