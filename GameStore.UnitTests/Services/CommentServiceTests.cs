using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Services;
using GameStore.UnitTests.Comparers;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Services;

public class CommentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly CommentService _commentService;

    #region TestData

    private readonly List<Comment> _comments = new List<Comment>()
    {
        new Comment()
        {
            Id = 1,
            Body = "Cool game!",
            Game = new Game()
            {
                Id = 1,
                Key = "Witcher3"
            },
            ParentCommentId = null
        },

        new Comment()
        {
            Id = 2,
            Body = "Couldn't agree more.",
            Game = new Game()
            {
                Id = 1,
                Key = "Witcher3"
            },
            ParentCommentId = 1
        },

        new Comment()
        {
            Id = 3,
            Body = "How to get a refund?????",
            Game = new Game()
            {
                Id = 1,
                Key = "Witcher3"
            },
            ParentCommentId = null
        },

        new Comment()
        {
            Id = 4,
            Body = "Not bad",
            Game = new Game()
            {
                Id = 2,
                Key = "TES5"
            },
            ParentCommentId = null
        },

        new Comment()
        {
            Id = 5,
            Body = "Too boring(",
            Game = new Game()
            {
                Id = 2,
                Key = "TES5"
            },
            ParentCommentId = null
        },

        new Comment()
        {
            Id = 6,
            Body = "......",
            Game = new Game()
            {
                Id = 2,
                Key = "TES5"
            },
            ParentCommentId = 5
        },

        new Comment()
        {
            Id = 7,
            Body = "Good, but too expensive.",
            Game = new Game()
            {
                Id = 3,
                Key = "GTA5"
            },
            ParentCommentId = null
        },

        new Comment()
        {
            Id = 8,
            Body = "Overrated",
            Game = new Game()
            {
                Id = 3,
                Key = "GTA5"
            },
            ParentCommentId = null
        },
    };

    public static IEnumerable<object[]> AddAsync_TestData()
    {
        yield return new object[]
        {
            "bekirov",
            "Witcher3",
            new CommentCreateModel()
            {
                Body = "Wow!"
            }
        };

        yield return new object[]
        {
            "bekirov",
            "GTA5",
            new CommentCreateModel()
            {
                Body = ":)"
            }
        };
    }

    #endregion

    public CommentServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();

        _commentService = new CommentService(_unitOfWorkMock.Object, _mapper);
    }

    [Theory]
    [InlineData("Witcher3")]
    [InlineData("GTA5")]
    public async Task GetByGameKeyAsync_ShouldReturnCorrectData(string gameKey)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.CommentRepository.GetByGameKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync((string key) => _comments.Where(c => c.Game.Key == key));

        var expected = _mapper.Map<IEnumerable<CommentModel>>(_comments.Where(c => c.Game.Key == gameKey));

        // Act
        var actual = await _commentService.GetByGameKeyAsync(gameKey);

        // Assert
        Assert.Equal(expected, actual, new CommentModelComparer());
    }

    [Theory]
    [MemberData(nameof(AddAsync_TestData))]
    public async Task AddAsync_ShouldCreateNewComment(string userName, string key, CommentCreateModel createData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync(new Game() { Id = 1 });

        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
                       .ReturnsAsync(new User() { Id = 1 });

        _unitOfWorkMock.Setup(x => x.CommentRepository.Add(It.IsAny<Comment>()));

        var expected = new CommentModel() { Body = createData.Body };

        // Act
        var actual = await _commentService.AddAsync(userName, key, createData);

        // Assert
        Assert.Equal(expected, actual, new CommentModelComparer());

        _unitOfWorkMock.Verify(x => x.CommentRepository.Add(It.IsAny<Comment>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
