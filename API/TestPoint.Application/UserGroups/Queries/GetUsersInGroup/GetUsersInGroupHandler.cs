using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Users;

namespace TestPoint.Application.UserGroups.Queries.GetUsersInGroup;

public class GetUsersInGroupHandler : IRequestHandler<GetUsersInGroupQuery, List<UserInformation>>
{
    private readonly IUnitOfWork _uow;

    public GetUsersInGroupHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<UserInformation>> Handle(GetUsersInGroupQuery request, CancellationToken cancellationToken)
    {
        var userGroup = await _uow.UserGroupRepository.FindOneAsync(ug => ug.Id == request.GroupId);

        if (userGroup is null)
        {
            throw new EntityNotFoundException($"User group with {request.GroupId} id was not found");
        }

        return userGroup.Users
            .Select(u => new UserInformation(u.Id, u.FirstName, u.LastName, u.Email, u.Avatar != null ? Convert.ToBase64String(u.Avatar) : null))
            .ToList();
    }
}
