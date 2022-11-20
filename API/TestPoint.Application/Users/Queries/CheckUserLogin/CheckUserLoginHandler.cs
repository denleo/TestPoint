using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

internal class CheckUserLoginHandler : IRequestHandler<CheckUserLoginQuery, CheckUserLoginResponse?>
{
    private readonly IUserDbContext _userDbContext;

    public CheckUserLoginHandler(IUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<CheckUserLoginResponse?> Handle(CheckUserLoginQuery request, CancellationToken cancellationToken)
    {
        User? user;
        if (request.Login.Contains("@"))
        {
            user = await _userDbContext.Users
                .Include(x => x.Login)
                .Where(u => u.Email == request.Login)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            user = await _userDbContext.Users
                .Include(x => x.Login)
                .Where(u => u.Login.Username == request.Login)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        if (user is null || !PasswordHelper.VerifyPassword(request.Password, user.Login.PasswordHash))
        {
            return null;
        }

        return new CheckUserLoginResponse
        {
            UserId = user.Id,
            Username = user.Login.Username
        };
    }
}