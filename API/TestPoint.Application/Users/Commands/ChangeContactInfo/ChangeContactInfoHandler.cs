using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Commands.ChangeContactInfo;

public class ChangeContactInfoHandler : IRequestHandler<ChangeContactInfoCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeContactInfoHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeContactInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id was not found");
        }

        if (user.FirstName != request.FirstName)
        {
            user.FirstName = request.FirstName;
        }
        if (user.LastName != request.LastName)
        {
            user.LastName = request.LastName;
        }
        if (user.Email != request.Email && !user.GoogleAuthenticated)
        {
            user.Email = request.Email;
            user.EmailConfirmed = false;
        }

        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
