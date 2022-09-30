using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GameStore.Application.Exceptions;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Services;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Services;

public class GameServiceTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    private readonly GameService _sut;

    public GameServiceTests()
    {
        _fixture = new Fixture();

        _fixture.Customize<Game>(game => 
            game.Without(g => g.Genres)
                .Without(g => g.PlatformTypes)
                .Without(g => g.Comments));

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();

        _sut = new GameService(_unitOfWorkMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnCorrectData()
    {
        // Arrange
        var games = _fixture.CreateMany<Game>(5);

        _unitOfWorkMock.Setup(x => x.GameRepository.GetWithPlatformsAndGenres())
                       .ReturnsAsync(games);

        var expected = _mapper.Map<IEnumerable<GameModel>>(games);

        // Act
        var actual = await _sut.GetAllAsync();

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task GetByGenreAsync_ShouldReturnCorrectData(int genreId)
    {
        // Arrange
        var games = _fixture.CreateMany<Game>(5);

        _unitOfWorkMock.Setup(x => x.GameRepository.GetByGenreAsync(It.IsAny<int>()))
                       .ReturnsAsync(games);

        var expected = _mapper.Map<IEnumerable<GameModel>>(games);

        // Act
        var actual = await _sut.GetByGenreAsync(genreId);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task GetByPlatformTypeAsync_ShouldReturnCorrectData(int platformId)
    {
        // Arrange
        var games = _fixture.CreateMany<Game>(5);

        _unitOfWorkMock.Setup(x => x.GameRepository.GetByPlatformAsync(It.IsAny<int>()))
                       .ReturnsAsync(games);

        var expected = _mapper.Map<IEnumerable<GameModel>>(games);

        // Act
        var actual = await _sut.GetByPlatformTypeAsync(platformId);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task GetByKeyAsync_ShouldReturnCorrectData(string gameKey)
    {
        // Arrange
        var game = _fixture.Create<Game>();

        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync(game);

        var expected = _mapper.Map<GameModel>(game);

        // Act
        var actual = await _sut.GetByKeyAsync(gameKey);

        // Assert
        actual.Should()
              .BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task GetByKeyAsync_ShouldThrowIfGameNotFound(string gameKey)
    {
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync((Game) null);

        var act = () => _sut.GetByKeyAsync(gameKey);

        await act.Should()
                 .ThrowAsync<NotFoundException>();
    }

    [Theory, AutoData]
    public async Task AddAsync_ShouldCreateNewGame(GameCreateModel createData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.Add(It.IsAny<Game>()));

        _unitOfWorkMock.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(_fixture.Create<Genre>());

        _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(_fixture.Create<PlatformType>());

        // Act
        await _sut.AddAsync(createData);

        // Assert
        _unitOfWorkMock.Verify(x => x.GameRepository.Add(It.IsAny<Game>()), Times.Once());
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory, AutoData]
    public async Task UpdateAsync_ShouldUpdateGame(string gameKey, GameCreateModel updateData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.Update(It.IsAny<Game>()));

        _unitOfWorkMock.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(_fixture.Create<Genre>());

        _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync(_fixture.Create<PlatformType>());

        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync(_fixture.Create<Game>());

        // Act
        await _sut.UpdateAsync(gameKey, updateData);

        // Assert
        _unitOfWorkMock.Verify(x => x.GameRepository.Update(It.IsAny<Game>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory, AutoData]
    public async Task DeleteAsync_ShouldDeleteGame(int gameId)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.DeleteByIdAsync(It.IsAny<int>()));

        // Act
        await _sut.DeleteAsync(gameId);

        // Assert
        _unitOfWorkMock.Verify(x => x.GameRepository.DeleteByIdAsync(gameId), Times.Once());
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once());
    }
}
