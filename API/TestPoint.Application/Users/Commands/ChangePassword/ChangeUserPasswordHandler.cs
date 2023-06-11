using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.ChangePassword;

public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeUserPasswordHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.FindOneAsync(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id does not exist.");
        }

        if (user.GoogleAuthenticated)
        {
            throw new ActionNotAllowedException("Can not change password for google authenticated account.");
        }

        if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Login.PasswordHash!))
        {
            throw new ActionNotAllowedException("Old password is not equal to the current one.");
        }

        var hash = PasswordHelper.ComputeHash(request.NewPassword);
        user.Login.PasswordHash = hash;
        user.Login.PasswordReseted = false;
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
