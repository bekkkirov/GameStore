using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Services;
using GameStore.UnitTests.Comparers;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Services;

public class GameServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly GameService _gameService;

    #region TestData

    private List<Game> _games = new List<Game>()
    {
        new Game()
        {
            Id = 1,
            Key = "Witcher3",
            Name = "Witcher 3",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1
                },

                new Genre()
                {
                    Id = 4
                },
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 1
                }
            }
        },

        new Game()
        {
            Id = 2,
            Key = "GTA5",
            Name = "GTA 5",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 2
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 2
                }
            }
        },

        new Game()
        {
            Id = 3,
            Key = "TES5",
            Name = "TES 5",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1
                },

                new Genre()
                {
                    Id = 2
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 2
                }
            }
        },

        new Game()
        {
            Id = 4,
            Key = "DeusEx",
            Name = "Deus Ex",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 3
                },

                new Genre()
                {
                    Id = 2
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 1
                },

                new PlatformType()
                {
                    Id = 2
                }
            }
        },

        new Game()
        {
            Id = 5,
            Key = "Bioshock",
            Name = "Bioshock",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 2
                }
            }
        },

        new Game()
        {
            Id = 6,
            Key = "Bioshock2",
            Name = "Bioshock 2",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1
                },

                new Genre()
                {
                    Id = 2
                },

                new Genre()
                {
                    Id = 3
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 3
                }
            }
        },

        new Game()
        {
            Id = 1,
            Key = "Cyberpunk2077",
            Name = "Cyberpunk 2077",
            Price = 9.99m,
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 2
                }
            },
            PlatformTypes = new List<PlatformType>()
            {
                new PlatformType()
                {
                    Id = 2
                }
            }
        },
    };

    public static IEnumerable<object[]> AddAsync_TestData()
    {
        yield return new object[]
        {
            new GameCreateModel()
            {
                Key = "TES4",
                Name = "TES4",
                Description = "DescriptionDescriptionDescriptionDescription.",
            }
        };

        yield return new object[]
        {
            new GameCreateModel()
            {
                Key = "GTA4",
                Name = "GTA4",
                Description = "DescriptionDescriptionDescriptionDescription.",
                GenreIds = new List<int>() {1, 5},
                PlatformIds = new List<int>() {1, 2, 3}
            }
        };
    }

    public static IEnumerable<object[]> UpdateAsync_TestData()
    {
        yield return new object[]
        {
            "TES5",
            new GameCreateModel()
            {
                Key = "TES5",
                Name = "TES5",
                Description = "New description.",
            }
        };

        yield return new object[]
        {
            "Witcher3",
            new GameCreateModel()
            {
                Key = "Witcher3",
                Name = "Witcher3",
                Description = "Updated description.",
                GenreIds = new List<int>() {1, 2},
                PlatformIds = new List<int>() {1}
            }
        };
    }

    #endregion

    public GameServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();

        _gameService = new GameService(_unitOfWorkMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnCorrectData()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetWithPlatformsAndGenres())
                       .ReturnsAsync(_games);

        var expected = _mapper.Map<IEnumerable<GameModel>>(_games);

        // Act
        var actual = await _gameService.GetAllAsync();

        // Assert
        Assert.Equal(expected, actual, new GameModelComparer());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task GetByGenreAsync_ShouldReturnCorrectData(int genreId)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByGenreAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => _games.Where(g => g.Genres.Any(genre => genre.Id == id)));

        var expected = _mapper.Map<IEnumerable<GameModel>>(_games.Where(g => g.Genres.Any(genre => genre.Id == genreId)));

        // Act
        var actual = await _gameService.GetByGenreAsync(genreId);

        // Assert
        Assert.Equal(expected, actual, new GameModelComparer());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetByPlatformTypeAsync_ShouldReturnCorrectData(int platformId)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByPlatformAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => _games.Where(g => g.PlatformTypes.Any(pt => pt.Id == id)));

        var expected = _mapper.Map<IEnumerable<GameModel>>(_games.Where(g => g.PlatformTypes.Any(pt => pt.Id == platformId))); ;

        // Act
        var actual = await _gameService.GetByPlatformTypeAsync(platformId);

        // Assert
        Assert.Equal(expected, actual, new GameModelComparer());
    }

    [Theory]
    [InlineData("Witcher3")]
    [InlineData("GTA5")]
    public async Task GetByKeyAsync_ShouldReturnCorrectData(string gameKey)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync((string key) => _games.FirstOrDefault(g => g.Key == key));

        var expected = _mapper.Map<GameModel>(_games.FirstOrDefault(g => g.Key == gameKey));

        // Act
        var actual = await _gameService.GetByKeyAsync(gameKey);

        // Assert
        Assert.Equal(expected, actual, new GameModelComparer());
    }

    [Theory]
    [InlineData("Civilization")]
    [InlineData("HOI4")]
    public async Task GetByKeyAsync_ShouldThrowIfGameNotFound(string gameKey)
    {
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync((string key) => _games.FirstOrDefault(g => g.Key == key));

        await Assert.ThrowsAsync<NotFoundException>(() => _gameService.GetByKeyAsync(gameKey));
    }

    [Theory]
    [MemberData(nameof(AddAsync_TestData))]
    public async Task AddAsync_ShouldCreateNewGame(GameCreateModel createData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.Add(It.IsAny<Game>()));

        _unitOfWorkMock.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => new Genre() { Id = id });

        _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => new PlatformType() { Id = id });

        var expected = new GameModel() { Key = createData.Key };

        // Act
        var actual = await _gameService.AddAsync(createData);

        // Assert
        Assert.Equal(expected, actual, new GameModelComparer());

        _unitOfWorkMock.Verify(x => x.GameRepository.Add(It.IsAny<Game>()), Times.Once());
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [MemberData(nameof(UpdateAsync_TestData))]
    public async Task UpdateAsync_ShouldUpdateGame(string gameKey, GameCreateModel updateData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.Update(It.IsAny<Game>()));

        _unitOfWorkMock.Setup(x => x.GenreRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => new Genre() { Id = id });

        _unitOfWorkMock.Setup(x => x.PlatformTypeRepository.GetByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((int id) => new PlatformType() { Id = id });

        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync((string key) => _games.FirstOrDefault(g => g.Key == key));

        // Act
        await _gameService.UpdateAsync(gameKey, updateData);

        // Assert
        _unitOfWorkMock.Verify(x => x.GameRepository.Update(It.IsAny<Game>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(22)]
    public async Task DeleteAsync_ShouldDeleteGame(int gameId)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.DeleteByIdAsync(It.IsAny<int>()));

        // Act
        await _gameService.DeleteAsync(gameId);

        // Assert
        _unitOfWorkMock.Verify(x => x.GameRepository.DeleteByIdAsync(gameId), Times.Once());
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once());
    }
}
