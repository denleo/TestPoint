using MediatR;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Queries.GetUserByEmail;
public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, User?>
{
    private readonly IUnitOfWork _uow;

    public GetUserByEmailHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
        await _uow.UserRepository
            .FindOneAsync(x => x.Email == request.Email);
}
