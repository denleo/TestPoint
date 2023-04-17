using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.Application.Users.Commands.CreateUser;

namespace TestPoint.Application.Tests.Users.Commands;

public class CreateUserHanderTests
{
    private readonly Mock<IUnitOfWork> _uow;
    private readonly IUserRepository _userRepository;

    public CreateUserHanderTests()
    {
        _uow = new Mock<IUnitOfWork>();

        _userRepository = new FakeUserRepository()
        {
            Users = new List<User>
            {
                new User
                {
                    Id = Guid.Empty,
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
                }
            }
        };

        _uow.Setup(x => x.UserRepository).Returns(_userRepository);
    }

    [Fact]
    public async void CreateUser_UserWithSuchUsernameExists_ThrowConflictException()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new CreateUserHandler(_uow.Object);
        var command = new CreateUserCommand
        {
            Username = "jdoe2002",
            Email = "wasd@gmail.com",
            FirstName = "Ivan",
            LastName = "Sergeenko",
            Password = "1234567890"
        };

        // Act 
        Task act() => handler.Handle(command, ct);

        // Assert
        await Assert.ThrowsAsync<EntityConflictException>(act);
    }

    [Fact]
    public async void CreateUser_UserWithSuchEmailExists_ThrowConflictException()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new CreateUserHandler(_uow.Object);
        var command = new CreateUserCommand
        {
            Username = "teste2003",
            Email = "test@test.com",
            FirstName = "Ivan",
            LastName = "Sergeenko",
            Password = "1234567890"
        };

        // Act 
        Task act() => handler.Handle(command, ct);

        // Assert
        await Assert.ThrowsAsync<EntityConflictException>(act);
    }

    [Fact]
    public async void CreateUser_ValidUser_SaveChanges()
    {
        // Arrange
        var ct = new CancellationToken(false);
        var handler = new CreateUserHandler(_uow.Object);
        var command = new CreateUserCommand
        {
            Username = "newuser",
            Email = "something@gmail.com",
            FirstName = "Ivan",
            LastName = "Sergeenko",
            Password = "1234567890"
        };

        // Act 
        await handler.Handle(command, ct);

        // Assert
        _uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}
