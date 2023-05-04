using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.UserGroups.Commands.AddUserToGroup;

public class AddUserToGroupHandler : IRequestHandler<AddUserToGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public AddUserToGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }
    public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
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

        if (userGroup.Users.Any(x => x.Id == request.UserId))
        {
            throw new EntityConflictException($"The user is already a member of this group.");
        }

        userGroup.Users.Add(user);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
