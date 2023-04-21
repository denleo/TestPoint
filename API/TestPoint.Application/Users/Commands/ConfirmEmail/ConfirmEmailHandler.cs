using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.ConfirmEmail;

internal class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUnitOfWork _uow;

    public ConfirmEmailHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .FindOneAsync(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id does not exist.");
        }

        if (user.Email != request.EmailForConfirmation)
        {
            throw new InvalidOperationException($"Actual user email is not equal to email under the confirmation ({request.EmailForConfirmation}).");
        }
         
        if (user.EmailConfirmed)
        {
            throw new EntityConflictException("User email is already confirmed.");
        }

        user.EmailConfirmed = true;
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}