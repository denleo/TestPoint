using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces;

namespace TestPoint.Application.Admins.Commands.ResetAdminPassword;

public class ResetAdminPasswordHandler : IRequestHandler<ResetAdminPasswordCommand, ResetAdminPasswordResponse>
{
    private readonly IAdminDbContext _adminDbContext;

    public ResetAdminPasswordHandler(IAdminDbContext adminDbContext)
    {
        _adminDbContext = adminDbContext;
    }

    public async Task<ResetAdminPasswordResponse> Handle(ResetAdminPasswordCommand request, CancellationToken cancellationToken)
    {
        var admin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(a => a.Login.Username == request.Username)
            .FirstOrDefaultAsync(cancellationToken);

        if (admin is null)
        {
            throw new EntityNotFoundException("Admin with such username doesn't exist");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();
        admin.Login.PasswordHash = PasswordHelper.ComputeHash(tempPassword);
        admin.IsPasswordReset = true;

        await _adminDbContext.SaveChangesAsync(cancellationToken);

        return new ResetAdminPasswordResponse
        {
            TempPassword = tempPassword
        };
    }
}