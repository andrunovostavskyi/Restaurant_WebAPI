using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurentUser()
    {
        //arrange
        DateOnly dateOfBirth= new DateOnly(1900,1,1);
        var httpContextAccesorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test"),
            new(ClaimTypes.Role, UserRoles.Admin),
            new(ClaimTypes.Role, UserRoles.User),
            new("Nationality", "German"),
            new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccesorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext(){
            User = user
        });

        var userContext = new UserContext(httpContextAccesorMock.Object);

        //act
        var result = userContext.GetCurrentUser();
        
        //assert
        result.Should().NotBeNull();
        result.Id.Should().Be("1");
        result.Email.Should().Be("test@test");
        result.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        result.Nationality.Should().Be("German");
        result.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact()]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperationException()
    {
        //arrange
        var httpContextAccesorMock = new Mock<IHttpContextAccessor>();
        httpContextAccesorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
        var userContext = new UserContext(httpContextAccesorMock.Object);

        //act
        Action action = () => userContext.GetCurrentUser(); 

        //assert
        action.Should().Throw<InvalidOperationException>().WithMessage("User context is not present");
    }
}