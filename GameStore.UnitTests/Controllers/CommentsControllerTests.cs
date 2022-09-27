using System.Collections.Generic;
using System.Threading.Tasks;
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
    private readonly Mock<ICommentService> _commentServiceMock = new Mock<ICommentService>();
    private readonly CommentsController _commentsController;

    public CommentsControllerTests()
    {
        _commentsController = new CommentsController(_commentServiceMock.Object);
    }

    #region TestData

    public static IEnumerable<object[]> AddComment_TestData()
    {
        yield return new object[]
        {
            "TES5",
            new CommentCreateModel()
            {
                Body = "Cool"
            }
        };

        yield return new object[]
        {
            "GTA5",
            new CommentCreateModel()
            {
                Body = "Wow"
            }
        };
    }

    #endregion

    [Theory]
    [MemberData(nameof(AddComment_TestData))]
    public async Task AddComment_ShouldReturnCreated(string gameKey, CommentCreateModel createData)
    {
        // Arrange
        _commentServiceMock.Setup(x => x.AddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CommentCreateModel>()));

        // Act
        var result = await _commentsController.AddComment(gameKey, createData);
        var objectResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status201Created, objectResult?.StatusCode);
    }

    [Theory]
    [InlineData("Witcher3")]
    [InlineData("Bioshock2")]
    public async Task GetByGameKey_ShouldReturnComments(string gameKey)
    {
        // Arrange
        _commentServiceMock.Setup(x => x.GetByGameKeyAsync(It.IsAny<string>()))
                           .ReturnsAsync(new List<CommentModel>());

        // Act
        var result = await _commentsController.GetByGameKey(gameKey);
        var objectResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
    }
}