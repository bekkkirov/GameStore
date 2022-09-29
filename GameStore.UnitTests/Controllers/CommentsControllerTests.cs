using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using GameStore.API.Controllers;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Controllers;

public class CommentsControllerTests
{
    private readonly Mock<ICommentService> _commentServiceMock;

    private readonly CommentsController _sut;

    public CommentsControllerTests()
    {
        _commentServiceMock = new Mock<ICommentService>();

        _sut = new CommentsController(_commentServiceMock.Object);
    }

    [Theory, AutoData]
    public async Task AddComment_ShouldReturnCreated(string gameKey, CommentCreateModel createData)
    {
        // Arrange
        _commentServiceMock.Setup(x => x.AddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CommentCreateModel>()));

        // Act
        var result = await _sut.AddComment(gameKey, createData);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult?.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

    [Theory, AutoData]
    public async Task GetByGameKey_ShouldReturnComments(string gameKey)
    {
        // Arrange
        _commentServiceMock.Setup(x => x.GetByGameKeyAsync(It.IsAny<string>()))
                           .ReturnsAsync(new List<CommentModel>());

        // Act
        var result = await _sut.GetByGameKey(gameKey);
        var objectResult = result.Result as ObjectResult;

        // Assert
        objectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}