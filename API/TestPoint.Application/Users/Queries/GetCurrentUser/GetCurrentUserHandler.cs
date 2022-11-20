using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    private readonly IUserDbContext _userDbContext;

    public GetCurrentUserHandler(IUserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userDbContext.Users
            .Include(x => x.Login)
            .Where(x => x.Id == request.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id does not exist");
        }

        return new GetCurrentUserResponse
        {
            UserId = user.Id,
            Username = user.Login.Username,
            PasswordReseted = user.Login.PasswordReseted,
            RegistryDate = user.Login.RegistryDate,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            Avatar = user.Avatar
        };
    }
}