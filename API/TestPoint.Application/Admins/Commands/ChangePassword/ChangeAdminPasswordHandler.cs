using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Commands.ChangePassword;

internal class ChangeAdminPasswordHandler : IRequestHandler<ChangeAdminPasswordCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeAdminPasswordHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeAdminPasswordCommand request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository.FindOneAsync(x => x.Id == request.AdminId);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Administrator with {request.AdminId} id does not exist.");
        }

        if (!PasswordHelper.VerifyPassword(request.OldPassword, admin.Login.PasswordHash))
        {
            throw new ActionNotAllowedException("Old password is not equal to the current one.");
        }

        var hash = PasswordHelper.ComputeHash(request.NewPassword);
        admin.Login.PasswordHash = hash;
        admin.Login.PasswordReseted = false;
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
