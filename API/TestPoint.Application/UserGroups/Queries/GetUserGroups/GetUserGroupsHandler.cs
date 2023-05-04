using MediatR;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.UserGroups.Queries.GetUserGroups;

public class GetUserGroupsHandler : IRequestHandler<GetUserGroupsQuery, List<UserGroupInformation>>
{
    private readonly IUnitOfWork _uow;

    public GetUserGroupsHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }
    public async Task<List<UserGroupInformation>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var userGroups = await _uow.UserGroupRepository
            .FilterByAsync(x => x.AdministratorId == request.AdminId);

        return userGroups
            .Select(ug => new UserGroupInformation(ug.Id, ug.Name, ug.Users.Count))
            .ToList();
    }
}
