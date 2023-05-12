using MediatR;

namespace TestPoint.Application.Users.Queries.FilterUsers;

public class FilterUsersQuery : IRequest<List<UserInformation>>
{
    public string FilterParameter { get; set; }
}
