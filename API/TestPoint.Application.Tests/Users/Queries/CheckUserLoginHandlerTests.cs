using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Queries.CheckUserLogin;

namespace TestPoint.Application.Tests.Users.Queries;

public class CheckUserLoginHandlerTests
{
    private readonly CheckUserLoginHandler _sut;
    private readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

    public CheckUserLoginHandlerTests()
    {
        var fakeUserRepo = new FakeUserRepository()
        {
            Users = new List<User>()
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
                        Id = Guid.Parse("2a3c6580-66f5-5c8b-bd3f-fc82391827ba"),
                        LoginType = LoginType.User,
                        Username = "jdoe2002",
                        PasswordHash = "RMaZSxsGXItxWUkdOrmXI7wpvP2FK0BycHzKTZ/4LHMDSD16rMuMav0W4+lutWHUCA4EERod2SWjFZN0WaxM3Q==5qZ7MKoJvlzzjregHFrPW/Cv+YGCYrvZyXt9phABRvs=",
                        PasswordReseted = false,
                        CreatedAt = DateTime.MaxValue,
                        UpdatedAt = DateTime.MaxValue,
                        RegistryDate = DateTime.MaxValue
                    }
                }
            }
        };

        _uow.Setup(x => x.UserRepository).Returns(fakeUserRepo);
        _sut = new CheckUserLoginHandler(_uow.Object);
    }

    [Theory]
    [InlineData("jdoe2002", "123123123123")]
    [InlineData("test@test.com", "123123123123")]
    public async void CheckUserLogin_ValidCredentials_ReturnUserId(string login, string password)
    {
        // Arrange
        var query = new CheckUserLoginQuery
        {
            Login = login,
            Password = password
        };

        // Act
        var result = await _sut.Handle(query, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"), result.UserId);
        Assert.Equal("jdoe2002", result.Username);
    }

    [Theory]
    [InlineData("testtest", "000000000000")]
    [InlineData("jdoe2002", "000000000000")]
    [InlineData("test@test.com", "000000000000")]
    public async void CheckUserLogin_InvalidCredentials_ReturnNull(string login, string password)
    {
        // Arrange
        var query = new CheckUserLoginQuery
        {
            Login = login,
            Password = password
        };

        // Act
        var result = await _sut.Handle(query, new CancellationToken());

        // Assert
        Assert.Null(result);
    }
}
