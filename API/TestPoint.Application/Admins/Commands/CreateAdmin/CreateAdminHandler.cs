using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces;
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
        var adminsWithSameLogin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(a => a.Login.Username == request.Username)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (adminsWithSameLogin.Count != 0)
        {
            throw new EntityExistsException("Username is already taken");
        }

        var newAdmin = new Administrator
        {
            Login = new SystemLogin
            {
                Username = request.Username,
                PasswordHash = PasswordEncryptionHelper.ComputeHash(request.Password),
                RegistryDate = DateTime.Now
            },
            IsPasswordReset = true
        };

        await _adminDbContext.Administrators.AddAsync(newAdmin, cancellationToken);
        await _adminDbContext.SaveChangesAsync(cancellationToken);

        return new CreateAdminResponse
        {
            AdminId = newAdmin.Id
        };
    }
}