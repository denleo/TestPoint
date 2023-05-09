using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.UserGroups.Commands.CreateUserGroup;

public class CreateUserGroupHandler : IRequestHandler<CreateUserGroupCommand, UserGroupInformation>
{
    private readonly IUnitOfWork _uow;

    public CreateUserGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<UserGroupInformation> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
    {
        var groupWithTheSameName = await _uow.UserGroupRepository
            .FindOneAsync(x => x.AdministratorId == request.AdministratorId && x.Name == request.GroupName);

        if (groupWithTheSameName is not null)
        {
            throw new EntityConflictException("Group with the same name already exists.");
        }

        var userGroup = new UserGroup
        {
            AdministratorId = request.AdministratorId,
            Name = request.GroupName
        };

        _uow.UserGroupRepository.Add(userGroup);
        await _uow.SaveChangesAsync(cancellationToken);

        return new UserGroupInformation(userGroup.Id, userGroup.Name, 0);
    }
}
