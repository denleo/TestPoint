using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.BindGoogleAccount;

public class BindGoogleAccountHandler : IRequestHandler<BindGoogleAccountCommand>
{
    private readonly IUnitOfWork _uow;

    public BindGoogleAccountHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(BindGoogleAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} doesn't exist.");
        }

        if (user.GoogleAccountMapping.GoogleSub == request.GoogleSub)
        {
            throw new EntityConflictException("User already has binding with this google account.");
        }

        var userWithSameSub = await _uow.UserRepository
            .FindOneAsync(x => x.GoogleAccountMapping.GoogleSub == request.GoogleSub && x.Id != request.UserId);

        if (userWithSameSub is not null)
        {
            throw new EntityConflictException("User with such google account already exists.");
        }

        user.GoogleAccountMapping.GoogleSub = request.GoogleSub;

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
