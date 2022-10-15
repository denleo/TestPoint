using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
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
        var userWithSameLogin = await _userDbContext.Users
            .Include(x => x.Login)
            .Where(u => u.Login.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (userWithSameLogin is not null)
        {
            throw new EntityExistsException("Username is already taken");
        }

        var userWithSameEmail = await _userDbContext.Users
            .Where(u => u.Email == request.Email)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

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