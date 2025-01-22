using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;
namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData("Admin")]
    [InlineData("User")]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string role)
    {
        //arrange
        var user = new CurrentUser("1","test@test",[UserRoles.Admin, UserRoles.User],null,null);

        //act
        var result = user.IsInRole(role);

        //assert
        result.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //arrange
        var user = new CurrentUser("1", "test@test", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var result = user.IsInRole(UserRoles.Owner);

        //assert
        result.Should().BeFalse();
    }


    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //arrange
        var user = new CurrentUser("1", "test@test", [UserRoles.Admin, UserRoles.User], null, null);

        //act
        var result = user.IsInRole(UserRoles.Admin.ToLower());

        //assert
        result.Should().BeFalse();
    }

}