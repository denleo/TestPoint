using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUnitOfWork _uow;
    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userWithSameUsername = await _uow.UserRepository
            .FindOneAsync(x => x.Login.Username == request.Username);

        if (userWithSameUsername is not null)
        {
            throw new EntityExistsException("Username is already taken");
        }

        var userWithSameEmail = await _uow.UserRepository
            .FindOneAsync(x => x.Email == request.Email);

        if (userWithSameEmail is not null)
        {
            throw new EntityExistsException("User with such email already exists");
        }

        var newUser = new User
        {
            Login = new SystemLogin
            {
                LoginType = LoginType.User,
                Username = request.Username,
                PasswordHash = PasswordHelper.ComputeHash(request.Password),
                PasswordReseted = false,
                RegistryDate = DateTime.Now
            },
            Email = request.Email,
            EmailConfirmed = false,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        _uow.UserRepository.Add(newUser);
        await _uow.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse
        {
            UserId = newUser.Id
        };
    }
}