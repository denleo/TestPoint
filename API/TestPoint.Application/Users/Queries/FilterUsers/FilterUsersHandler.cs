using MediatR;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Queries.FilterUsers;

public class FilterUsersHandler : IRequestHandler<FilterUsersQuery, List<UserInformationShort>>
{
    private readonly IUnitOfWork _uow;

    public FilterUsersHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<UserInformationShort>> Handle(FilterUsersQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.FilterParameter))
        {
            return new List<UserInformationShort>(0);
        }

        var users = await _uow.UserRepository.FilterUsersByFIO(request.FilterParameter);
        return users;
    }
}
