using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    private readonly IUnitOfWork _uow;

    public GetCurrentUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository
            .FindOneAsync(x => x.Id == request.UserId);

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
            GoogleAuthenticated = user.GoogleAuthenticated,
            Base64Avatar = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : null
        };
    }
}