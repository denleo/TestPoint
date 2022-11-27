using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Queries.GetCurrentAdmin;

public class GetCurrentAdminHandler : IRequestHandler<GetCurrentAdminQuery, GetCurrentAdminResponse>
{
    private readonly IUnitOfWork _uow;

    public GetCurrentAdminHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<GetCurrentAdminResponse> Handle(GetCurrentAdminQuery request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository
            .FindOneAsync(x => x.Id == request.AdminId);

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