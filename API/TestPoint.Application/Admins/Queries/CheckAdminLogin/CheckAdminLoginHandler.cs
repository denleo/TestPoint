using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginHandler : IRequestHandler<CheckAdminLoginQuery, CheckAdminLoginResponse?>
{
    private readonly IUnitOfWork _uow;

    public CheckAdminLoginHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<CheckAdminLoginResponse?> Handle(CheckAdminLoginQuery request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository
            .FindOneAsync(x => x.Login.Username == request.Username);

        if (admin is null || !PasswordHelper.VerifyPassword(request.Password, admin.Login.PasswordHash))
        {
            return null;
        }

        return new CheckAdminLoginResponse
        {
            AdminId = admin.Id,
            Username = admin.Login.Username
        };
    }
}