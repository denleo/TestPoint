using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Queries.GetCurrentAdmin;

public class GetCurrentAdminHandler : IRequestHandler<GetCurrentAdminQuery, GetCurrentAdminResponse>
{
    private readonly IAdminDbContext _adminDbContext;

    public GetCurrentAdminHandler(IAdminDbContext adminDbContext)
    {
        _adminDbContext = adminDbContext;
    }

    public async Task<GetCurrentAdminResponse> Handle(GetCurrentAdminQuery request, CancellationToken cancellationToken)
    {
        var admin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(x => x.Id == request.AdminId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Administrator with {request.AdminId} id does not exist");
        }

        return new GetCurrentAdminResponse
        {
            AdminId = admin.Id,
            Username = admin.Login.Username,
            PasswordReseted = admin.Login.PasswordReseted,
            RegistryDate = admin.Login.RegistryDate
        };
    }
}