using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork _uow;
    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userWithSameUsername = await _uow.UserRepository
            .FindOneAsync(x => x.Login.Username == request.Username);

        if (userWithSameUsername is not null)
        {
            throw new EntityConflictException("Username is already taken.");
        }

        var userWithSameEmail = await _uow.UserRepository
            .FindOneAsync(x => x.Email == request.Email);

        if (userWithSameEmail is not null)
        {
            throw new EntityConflictException("User with such email already exists.");
        }

        var newUser = new User
        {
            Login = new SystemLogin
            {
                LoginType = LoginType.User,
                Username = request.IsGoogleAccount ? request.Email.Split('@')[0] : request.Username!,
                PasswordHash = request.IsGoogleAccount ? null : PasswordHelper.ComputeHash(request.Password!),
                PasswordReseted = false,
                RegistryDate = DateTime.Now
            },
            GoogleAuthenticated = request.IsGoogleAccount,
            Email = request.Email,
            EmailConfirmed = request.IsGoogleAccount,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Avatar = request.GoogleAvatar
        };

        _uow.UserRepository.Add(newUser);
        await _uow.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}