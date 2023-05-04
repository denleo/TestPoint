using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Commands.ChangePassword;

namespace TestPoint.Application.Tests.Users.Commands;

public class ChangeUserPasswordHandlerTests
{
    private readonly ChangeUserPasswordHandler _sut;
    private readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

    public ChangeUserPasswordHandlerTests()
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
        _sut = new ChangeUserPasswordHandler(_uow.Object);
    }

    [Fact]
    public async void ChangeUserPassword_UserDoesNotExist_ThrowEntityNotFoundException()
    {
        // Arrange
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Empty,
            OldPassword = "1234567890",
            NewPassword = "1234567890"
        };

        // Act 
        Task act() => _sut.Handle(command, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }

    [Fact]
    public async void ChangeUserPassword_WrongOldPassword_ThrowActionNotAllowedException()
    {
        // Arrange
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            OldPassword = "1234567890",
            NewPassword = "1234567890"
        };

        // Act 
        Task act() => _sut.Handle(command, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<ActionNotAllowedException>(act);
    }

    [Fact]
    public async void ChangeUserPassword_ValidCommand_SaveChanges()
    {
        // Arrange
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            OldPassword = "123123123123",
            NewPassword = "1234567890"
        };

        // Act 
        await _sut.Handle(command, new CancellationToken());

        // Assert
        _uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
