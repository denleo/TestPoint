using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

internal class CheckUserLoginHandler : IRequestHandler<CheckUserLoginQuery, CheckUserLoginResponse?>
{
    private readonly IUnitOfWork _uow;

    public CheckUserLoginHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<CheckUserLoginResponse?> Handle(CheckUserLoginQuery request, CancellationToken cancellationToken)
    {
        User? user;
        if (request.Login.Contains("@"))
        {
            user = await _uow.UserRepository
                .FindOneAsync(x => x.Email == request.Login);
        }
        else
        {
            user = await _uow.UserRepository
                .FindOneAsync(x => x.Login.Username == request.Login);
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