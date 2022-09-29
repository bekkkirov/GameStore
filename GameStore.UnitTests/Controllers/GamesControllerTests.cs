using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
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

public class GamesControllerTests
{
    private readonly Fixture _fixture;

    private readonly Mock<IGameService> _gameServiceMock;
    private readonly GamesController _gamesController;

    public GamesControllerTests()
    {
        _fixture = new Fixture();
        _gameServiceMock = new Mock<IGameService>();

        _gamesController = new GamesController(_gameServiceMock.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnCorrectGames()
    {
        // Arrange
        var games = _fixture.CreateMany<GameModel>(5).ToList();

        _gameServiceMock.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(games);

        // Act
        var result = await _gamesController.Get();
        var objectResult = result.Result as ObjectResult;
        var actualGames = objectResult?.Value as IEnumerable<GameModel>;

        // Assert
        actualGames?.Should()
                    .BeEquivalentTo(games);

        objectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Theory, AutoData]
    public async Task GetByKey_ShouldReturnCorrectGames(string gameKey)
    {
        // Arrange
        var game = _fixture.Create<GameModel>();

        _gameServiceMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>()))
                        .ReturnsAsync(game);

        // Act
        var result = await _gamesController.GetByKey(gameKey);
        var objectResult = result.Result as ObjectResult;
        var actualGame = objectResult?.Value as GameModel;

        // Assert
        actualGame.Should()
                  .BeEquivalentTo(game);

        objectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Theory, AutoData]
    public async Task Delete_ShouldDeleteGame(int gameId)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.DeleteAsync(It.IsAny<int>()));

        // Act
        var result = await _gamesController.Delete(gameId);
        var objectResult = result as NoContentResult;

        // Assert
        objectResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }

    [Theory, AutoData]
    public async Task Add_ShouldReturnCreatedGame(GameCreateModel createData)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.AddAsync(It.IsAny<GameCreateModel>()))
                        .ReturnsAsync(new GameModel() { Key = createData.Key });

        var expected = new GameModel() { Key = createData.Key };

        // Act
        var result = await _gamesController.Add(createData);
        var objectResult = result.Result as ObjectResult;

        // Assert
        result.Should().BeEquivalentTo(expected);
        objectResult?.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

    [Theory, AutoData]
    public async Task Update_ShouldUpdateGame(string key, GameCreateModel updateData)
    {
        // Arrange
        _gameServiceMock.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<GameCreateModel>()));

        // Act
        var result = await _gamesController.Update(key, updateData);
        var objectResult = result as OkResult;

        // Assert
        objectResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}