using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurants.API.Middlewares.Tests;

public class ErrorHandlingMiddleTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddle>> _loggerMock;
    private readonly DefaultHttpContext _context;

    public ErrorHandlingMiddleTests()
    {
        _loggerMock = new Mock<ILogger<ErrorHandlingMiddle>>();
        _context = new DefaultHttpContext();
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThown_ShouldGet404StatusCode()
    {
        //arrange
        var middleWare = new ErrorHandlingMiddle(_loggerMock.Object);
        var notFoundExceptions = new NotFoundException(nameof(Restaurant),"1");


        //act
        await middleWare.InvokeAsync(_context, _=> throw notFoundExceptions);

        //asert
        _context.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNoAccessThown_ShouldGet403StatusCode()
    {
        //arrange
        var middleWare = new ErrorHandlingMiddle(_loggerMock.Object);
        var noAccess = new NotAcces();


        //act
        await middleWare.InvokeAsync(_context, _=> throw noAccess);

        //asert
        _context.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }

    [Fact()]
    public async Task InvokeAsync_WhenExceptionsThown_ShouldGet500StatusCode()
    {
        //arrange
        var middleWare = new ErrorHandlingMiddle(_loggerMock.Object);
        var exceptions = new Exception();


        //act
        await middleWare.InvokeAsync(_context, _=>throw exceptions);

        //asert
        _context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionsThown_ShouldCallNextDelegate()
    {
        //arrange
        var middleWare = new ErrorHandlingMiddle(_loggerMock.Object);
        var nextDelegate = new Mock<RequestDelegate>();


        //act
        await middleWare.InvokeAsync(_context, nextDelegate.Object);

        //asert
        nextDelegate.Verify(v => v.Invoke(_context), Times.Once);
    }
}