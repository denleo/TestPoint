using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Commands.ConfirmEmail;

namespace TestPoint.Application.Tests.Users.Commands;

public class ConfirmEmailHandlerTests
{
    private readonly ConfirmEmailHandler _sut;
    private readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

    public ConfirmEmailHandlerTests()
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
                        PasswordHash = "######################",
                        PasswordReseted = false,
                        CreatedAt = DateTime.MaxValue,
                        UpdatedAt = DateTime.MaxValue,
                        RegistryDate = DateTime.MaxValue
                    }
                },
                new User
                {
                    Id = Guid.Parse("c7119f62-f212-47d1-bd35-73f141167299"),
                    FirstName = "Deniel",
                    LastName = "Karnegi",
                    Avatar = null,
                    Email = "wasd@wasd.com",
                    EmailConfirmed = false,
                    CreatedAt = DateTime.MaxValue,
                    UpdatedAt = DateTime.MaxValue,
                    Login = new SystemLogin
                    {
                        Id = Guid.Empty,
                        LoginType = LoginType.User,
                        Username = "wasd2000",
                        PasswordHash = "######################",
                        PasswordReseted = false,
                        CreatedAt = DateTime.MaxValue,
                        UpdatedAt = DateTime.MaxValue,
                        RegistryDate = DateTime.MaxValue
                    }
                },
            }
        };

        _uow.Setup(x => x.UserRepository).Returns(fakeUserRepo);
        _sut = new ConfirmEmailHandler(_uow.Object);
    }

    [Fact]
    public async void ConfirmUserEmail_UserDoesNotExist_ThrowEntityNotFoundException()
    {
        // Arrange
        var command = new ConfirmEmailCommand
        {
            UserId = Guid.Empty,
            EmailForConfirmation = "test@test.com"
        };

        // Act 
        Task act() => _sut.Handle(command, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(act);
    }

    [Fact]
    public async void ConfirmUserEmail_EmailsAreNotTheSame_ThrowInvalidOperationException()
    {
        // Arrange
        var command = new ConfirmEmailCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            EmailForConfirmation = "test2@test2.com"
        };

        // Act 
        Task act() => _sut.Handle(command, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async void ConfirmUserEmail_EmailAlreadyConfirmed_ThrowEntityConflictException()
    {
        // Arrange
        var command = new ConfirmEmailCommand
        {
            UserId = Guid.Parse("1e1c6580-66f5-4c8b-bd3f-fc82394827be"),
            EmailForConfirmation = "test@test.com"
        };

        // Act 
        Task act() => _sut.Handle(command, new CancellationToken());

        // Assert
        await Assert.ThrowsAsync<EntityConflictException>(act);
    }

    [Fact]
    public async void ConfirmUserEmail_ValidCommand_SaveChanges()
    {
        // Arrange
        var command = new ConfirmEmailCommand
        {
            UserId = Guid.Parse("c7119f62-f212-47d1-bd35-73f141167299"),
            EmailForConfirmation = "wasd@wasd.com"
        };

        // Act 
        await _sut.Handle(command, new CancellationToken());

        // Assert
        _uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}
