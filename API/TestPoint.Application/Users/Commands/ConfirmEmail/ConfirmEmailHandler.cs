using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.ConfirmEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserDbContext _userDbContext;

    public ConfirmEmailHandler(IUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users
            .Where(x => x.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User with does not exist.");
        }

        if (user.Email != request.EmailForConfirmation)
        {
            throw new InvalidOperationException($"Actual user email is not equal to email under the confirmation ({request.EmailForConfirmation}).");
        }

        if (user.EmailConfirmed)
        {
            throw new EmailConfirmationException("User email is already confirmed.");
        }

        user.EmailConfirmed = true;
        await _userDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}