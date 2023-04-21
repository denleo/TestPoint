using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Commands.ResetAdminPassword;

internal class ResetAdminPasswordHandler : IRequestHandler<ResetAdminPasswordCommand, ResetAdminPasswordResponse>
{
    private readonly IUnitOfWork _uow;

    public ResetAdminPasswordHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<ResetAdminPasswordResponse> Handle(ResetAdminPasswordCommand request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository
            .FindOneAsync(x => x.Login.Username == request.Username);

        if (admin is null)
        {
            throw new EntityNotFoundException("Admin with such username doesn't exist.");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();
        admin.Login.PasswordHash = PasswordHelper.ComputeHash(tempPassword);
        admin.Login.PasswordReseted = true;

        await _uow.SaveChangesAsync(cancellationToken);

        return new ResetAdminPasswordResponse
        {
            TempPassword = tempPassword
        };
    }
}