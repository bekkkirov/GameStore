using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Services;
using Moq;
using Xunit;

namespace GameStore.UnitTests.Services;

public class CommentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    private readonly CommentService _sut;

    public CommentServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapper = UnitTestsHelpers.CreateMapperProfile();

        _sut = new CommentService(_unitOfWorkMock.Object, _mapper);
    }

    [Theory, AutoData]
    public async Task GetByGameKeyAsync_ShouldReturnCorrectData(string gameKey)
    {
        // Arrange
        var comments = _fixture.CreateMany<Comment>(5);

        _unitOfWorkMock.Setup(x => x.CommentRepository.GetByGameKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync(comments);

        var expected = _mapper.Map<IEnumerable<CommentModel>>(comments);

        // Act
        var actual = await _sut.GetByGameKeyAsync(gameKey);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task AddAsync_ShouldCreateNewComment(string userName, string key, CommentCreateModel createData)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.GameRepository.GetByKeyAsync(It.IsAny<string>()))
                       .ReturnsAsync(_fixture.Create<Game>());

        _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
                       .ReturnsAsync(_fixture.Create<User>());

        _unitOfWorkMock.Setup(x => x.CommentRepository.Add(It.IsAny<Comment>()));

        var expected = new CommentModel() { Body = createData.Body };

        // Act
        var actual = await _sut.AddAsync(userName, key, createData);

        // Assert
        actual.Body.Should().Be(expected.Body);

        _unitOfWorkMock.Verify(x => x.CommentRepository.Add(It.IsAny<Comment>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
