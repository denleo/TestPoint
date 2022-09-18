using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Enums;
using TestPoint.Application.Interfaces;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginHandler : IRequestHandler<CheckAdminLoginQuery, CheckAdminLoginResponse?>
{
    private readonly IAdminDbContext _adminDbContext;

    public CheckAdminLoginHandler(IAdminDbContext adminDbContext)
    {
        _adminDbContext = adminDbContext;
    }

    public async Task<CheckAdminLoginResponse?> Handle(CheckAdminLoginQuery request, CancellationToken cancellationToken)
    {
        var admin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(a => a.Login.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);


        if (admin is null || !PasswordEncryptionHelper.VerifyPassword(request.Password, admin.Login.PasswordHash))
        {
            return null;
        }

        return new CheckAdminLoginResponse
        {
            Username = admin.Login.Username,
            Role = AccessRole.Administrator
        };
    }
}