using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotBeValidationProblem()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            ContactEmail = "test@test",
            ContactNumber = "212121212",
            Category = "Germany",
            PostalCode = "333-22"
        };
        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForValidCommand_ShouldBeValidationProblem()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            ContactEmail = "test",
            ContactNumber = "cbefged",
            Category = "Germ",
            PostalCode = "33322"
        };
        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c=>c.Name);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory()]
    [InlineData("Germany")]
    [InlineData("Ukraine")]
    [InlineData("Indian")]
    [InlineData("English")]
    public void Validator_ForCategoryValidCommand_ShouldNotBeValidatorProblemForCategory(string category)
    {
        //arrange
        var rest = new CreateRestaurantCommand
        {
            Category = category
        };
        var validator = new CreateRestaurantCommandValidator();

        //act 
        var result = validator.TestValidate(rest);
        
        //assert
        result.ShouldNotHaveValidationErrorFor(c=>c.Category);
    }

    [Theory()]
    [InlineData("33333")]
    [InlineData("33-222")]
    [InlineData("1-2222")]
    [InlineData("4423-1")]
    public void Validator_ForPostalCodeValidCommand_ShouldBeValidatorProblemForPostalCode(string postalCode)
    {
        //arrange
        var rest = new CreateRestaurantCommand
        {
            PostalCode = postalCode
        };
        var validator = new CreateRestaurantCommandValidator();

        //act 
        var result = validator.TestValidate(rest);

        //assert
        result.ShouldHaveValidationErrorFor(c=>c.PostalCode);
    }

}