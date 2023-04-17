using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Commands.ChangePassword;

namespace TestPoint.Application.Tests.Users.Commands;

public class ChangeUserPasswordHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow;
    private readonly FakeUserRepository _userRepository;

    public ChangeUserPasswordHandlerTests()
    {
        _uow = new Mock<IUnitOfWork>();

        _userRepository = new FakeUserRepository()
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

        _uow.Setup(x => x.UserRepository).Returns(_userRepository);
    }

    [Fact]
    public async void ChangeUserPassword_UserDoesNotExist_ThrowEntityNotFoundException()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new ChangeUserPasswordHandler(_uow.Object);
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Empty,
            OldPassword = "1234567890",
            NewPassword = "1234567890"
        };

        // Act 
        Task act() => handler.Handle(command, ct);

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }

    [Fact]
    public async void ChangeUserPassword_WrongOldPassword_ThrowActionNotAllowedException()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new ChangeUserPasswordHandler(_uow.Object);
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            OldPassword = "1234567890",
            NewPassword = "1234567890"
        };

        // Act 
        Task act() => handler.Handle(command, ct);

        // Assert
        await Assert.ThrowsAsync<ActionNotAllowedException>(act);
    }

    [Fact]
    public async void ChangeUserPassword_ValidCommand_SaveChanges()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new ChangeUserPasswordHandler(_uow.Object);
        var command = new ChangeUserPasswordCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            OldPassword = "123123123123",
            NewPassword = "1234567890"
        };

        // Act 
        await handler.Handle(command, ct);

        // Assert
        _uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
