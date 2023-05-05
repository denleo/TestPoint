using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.ChangeAvatar;

public class ChangeUserAvatarHandler : IRequestHandler<ChangeUserAvatarCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeUserAvatarHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id was not found");
        }

        var binaryAvatar = Convert.FromBase64String(request.Base64Avatar);
        user.Avatar = binaryAvatar;

        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
