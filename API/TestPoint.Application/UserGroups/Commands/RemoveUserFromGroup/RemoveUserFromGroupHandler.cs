using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.UserGroups.Commands.RemoveUserFromGroup;

internal class RemoveUserFromGroupHandler : IRequestHandler<RemoveUserFromGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public RemoveUserFromGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .FindOneAsync(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id was not found.");
        }

        var userGroup = await _uow.UserGroupRepository
            .FindOneAsync(x => x.Id == request.GroupId);

        if (userGroup is null)
        {
            throw new EntityNotFoundException($"User group with {request.UserId} id was not found.");
        }

        if (userGroup.Users.All(x => x.Id != request.UserId))
        {
            throw new EntityConflictException($"User is not a member of this group.");
        }

        userGroup.Users.Remove(user);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
