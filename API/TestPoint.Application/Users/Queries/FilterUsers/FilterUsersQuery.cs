using MediatR;

namespace TestPoint.Application.Users.Queries.FilterUsers;

public class FilterUsersQuery : IRequest<List<UserInformationShort>>
{
    public string FilterParameter { get; set; }
}
