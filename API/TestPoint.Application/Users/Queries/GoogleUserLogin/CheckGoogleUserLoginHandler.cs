using MediatR;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users.Queries.CheckGoogleUserLogin;
using TestPoint.Application.Users.Queries.CheckUserLogin;

namespace TestPoint.Application.Users.Queries.GoogleUserLogin;

public class CheckGoogleUserLoginHandler : IRequestHandler<CheckGoogleUserLoginQuery, CheckUserLoginResponse?>
{
    private readonly IUnitOfWork _uow;

    public CheckGoogleUserLoginHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<CheckUserLoginResponse?> Handle(CheckGoogleUserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .FindOneAsync(x => x.GoogleAccountMapping.GoogleSub == request.GoogleSub);

        return user is null
            ? null
            : new CheckUserLoginResponse
            {
                UserId = user.Id,
                Username = user.Login.Username
            };
    }
}
