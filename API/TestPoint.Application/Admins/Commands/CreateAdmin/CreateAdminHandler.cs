using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Admins.Commands.CreateAdmin;

public class CreateAdminHandler : IRequestHandler<CreateAdminCommand, CreateAdminResponse>
{
    private readonly IAdminDbContext _adminDbContext;

    public CreateAdminHandler(IAdminDbContext adminDbContext)
    {
        _adminDbContext = adminDbContext;
    }

    public async Task<CreateAdminResponse> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var adminWithSameLogin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(a => a.Login.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (adminWithSameLogin is not null)
        {
            throw new EntityExistsException("Username is already taken");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();

        var newAdmin = new Administrator
        {
            Login = new SystemLogin
            {
                LoginType = LoginType.Administrator,
                Username = request.Username,
                PasswordHash = PasswordHelper.ComputeHash(tempPassword),
                RegistryDate = DateTime.Now
            },
            IsPasswordReset = true
        };

        await _adminDbContext.Administrators.AddAsync(newAdmin, cancellationToken);
        await _adminDbContext.SaveChangesAsync(cancellationToken);

        return new CreateAdminResponse
        {
            AdminId = newAdmin.Id,
            TempPassword = tempPassword
        };
    }
}