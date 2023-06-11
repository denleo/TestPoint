using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.UnbindGoogleAccount;

public class UnbindGoogleAccountHandler : IRequestHandler<UnbindGoogleAccountCommand>
{
    private readonly IUnitOfWork _uow;

    public UnbindGoogleAccountHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }
    public async Task<Unit> Handle(UnbindGoogleAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} doesn't exist.");
        }

        user.GoogleAccountMapping.GoogleSub = null;

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
