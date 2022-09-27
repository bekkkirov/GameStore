using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.API.Controllers;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.UnitTests.Comparers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Controllers;

public class GamesControllerTests
{
    private readonly Mock<IGameService> _gameServiceMock;
    private readonly GamesController _gamesController;

    public GamesControllerTests()
    {
        _gameServiceMock = new Mock<IGameService>();

        _gamesController = new GamesController(_gameServiceMock.Object);
    }

    #region TestData

    private readonly List<GameModel> _games = new List<GameModel>()
    {
        new GameModel()
        {
            Id = 1,
            Key = "Witcher3"
        },

        new GameModel()
        {
            Id = 2,
            Key = "GTA5"
        },

        new GameModel()
        {
            Id = 3,
            Key = "Bioshock"
        },

        new GameModel()
        {
            Id = 4,
            Key = "Bioshock2"
        },

        new GameModel()
        {
            Id = 5,
            Key = "HOI4"
        }
    };

    public static IEnumerable<object[]> Add_TestData()
    {
        yield return new object[]
        {
            new GameCreateModel()
            {
                Key = "NewGame1"
            }
        };

        yield return new object[]
        {
            new GameCreateModel()
            {
                Key = "NewGame2"
            }
        };
    }

    public static IEnumerable<object[]> Update_TestData()
    {
        yield return new object[]
        {
            "SomeGame1",
            new GameCreateModel()
            {
                Key = "SomeGame1",
                Name = "NewName",
            }
        };

        yield return new object[]
        {
            "SomeGame2",
            new GameCreateModel()
            {
                Key = "SomeGame2",
                Description = "NewDescription"
            }
        };
    }

    #endregion

    [Fact]
    public async Task Get_ShouldReturnCorrectGames()
    {
        // Arrange
        _gameServiceMock.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(_games);

        // Act
        var result = await _gamesController.Get();
        var objectResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(_games, objectResult?.Value as IEnumerable<GameModel>, new GameModelComparer());
        Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
    }

    [Theory]
    [InlineData("HOI4")]
    [InlineData("Witcher3")]
    [InlineData("Bioshock")]
    public async Task GetByKey_ShouldReturnCorrectGames(string gameKey)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>()))
                        .ReturnsAsync((string key) => _games.FirstOrDefault(g => g.Key == key));

        var expected = _games.FirstOrDefault(g => g.Key == gameKey);

        // Act
        var result = await _gamesController.GetByKey(gameKey);
        var objectResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(expected, objectResult?.Value as GameModel, new GameModelComparer());
        Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task Delete_ShouldDeleteGame(int gameId)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.DeleteAsync(It.IsAny<int>()));

        // Act
        var result = await _gamesController.Delete(gameId);
        var objectResult = result as NoContentResult;

        // Assert
        Assert.Equal(StatusCodes.Status204NoContent, objectResult?.StatusCode);
    }

    [Theory]
    [MemberData(nameof(Add_TestData))]
    public async Task Add_ShouldReturnCreatedGame(GameCreateModel createData)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.AddAsync(It.IsAny<GameCreateModel>()))
                        .ReturnsAsync(new GameModel() {Key = createData.Key});

        var expected = new GameModel() { Key = createData.Key };

        // Act
        var result = await _gamesController.Add(createData);
        var objectResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(expected, objectResult?.Value as GameModel, new GameModelComparer());
        Assert.Equal(StatusCodes.Status201Created, objectResult?.StatusCode);
    }

    [Theory]
    [MemberData(nameof(Update_TestData))]
    public async Task Update_ShouldUpdateGame(string key, GameCreateModel updateData)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<GameCreateModel>()));

        // Act
        var result = await _gamesController.Update(key, updateData);
        var objectResult = result as OkResult;

        // Assert
        Assert.Equal(StatusCodes.Status200OK, objectResult?.StatusCode);
    }
}