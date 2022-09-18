using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserDbContext _userDbContext;
    public CreateUserHandler(IUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var usersWithSameLogin = await _userDbContext.Users
            .Include(x => x.Login)
            .Where(u => u.Login.Username == request.Username)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (usersWithSameLogin.Count != 0)
        {
            throw new EntityExistsException("Username is already taken");
        }

        var usersWithSameEmail = await _userDbContext.Users
            .Where(u => u.Email == request.Email)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (usersWithSameEmail.Count != 0)
        {
            throw new EntityExistsException("User with such email already exists");
        }

        var newUser = new User
        {
            Login = new SystemLogin
            {
                Username = request.Username,
                PasswordHash = PasswordEncryptionHelper.ComputeHash(request.Password),
                RegistryDate = DateTime.Now
            },
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _userDbContext.Users.AddAsync(newUser, cancellationToken);
        await _userDbContext.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse
        {
            UserId = newUser.Id
        };
    }
}