using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.UserGroups.Commands.DeleteUserGroup;

internal class DeleteUserGroupHandler : IRequestHandler<DeleteUserGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteUserGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
    {
        var userGroup = await _uow.UserGroupRepository
            .FindOneAsync(x => x.Id == request.GroupId);

        if (userGroup is null)
        {
            throw new EntityNotFoundException($"User group with {request.GroupId} was not found.");
        }

        _uow.UserGroupRepository.Remove(userGroup);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
