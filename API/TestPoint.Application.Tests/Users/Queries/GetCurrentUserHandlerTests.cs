using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Queries.GetCurrentUser;

namespace TestPoint.Application.Tests.Users.Queries;

public class GetCurrentUserHandlerTests
{
    private readonly GetCurrentUserHandler _sut;
    private readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

    public GetCurrentUserHandlerTests()
    {
        var fakeUserRepo = new FakeUserRepository()
        {
            Users = new List<User>
            {
                new User
                {
                    Id = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
                    FirstName = "Jhon",
                    LastName = "Doe",
                    Avatar = null,
                    Email = "test@test.com",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.MaxValue,
                    UpdatedAt = DateTime.MaxValue,
                    Login = new SystemLogin
                    {
                        Id = Guid.Empty,
                        LoginType = LoginType.User,
                        Username = "jdoe2002",
                        PasswordHash = "###############################",
                        PasswordReseted = false,
                        CreatedAt = DateTime.MaxValue,
                        UpdatedAt = DateTime.MaxValue,
                        RegistryDate = DateTime.MaxValue
                    }
                }
            }
        };

        _uow.Setup(x => x.UserRepository).Returns(fakeUserRepo);
        _sut = new GetCurrentUserHandler(_uow.Object);
    }

    [Fact]
    public async void GetCurrentUser_UserExists_ReturnUserData()
    {
        // Arrange
        var query = new GetCurrentUserQuery
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be")
        };

        // Act
        var result = await _sut.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"), result.UserId);
        Assert.Equal("Jhon", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("jdoe2002", result.Username);
        Assert.Equal(DateTime.MaxValue, result.RegistryDate);
        Assert.Equal("test@test.com", result.Email);
        Assert.True(result.EmailConfirmed);
        Assert.Null(result.Avatar);
    }

    [Theory]
    [InlineData("273c6480-66f5-4c8b-bd3f-fc82394827be")]
    [InlineData("123c6480-66f5-4c8b-bd3f-fc82394827be")]
    [InlineData("153c6480-66f5-4c8b-bd3f-fc82394827be")]
    public async void GetCurrentUser_UserDoesNotExist_ThrowEntityNotFoundException(string guid)
    {
        // Arrange
        var query = new GetCurrentUserQuery
        {
            UserId = Guid.Parse(guid)
        };

        // Act
        var act = () => _sut.Handle(query, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }
}
