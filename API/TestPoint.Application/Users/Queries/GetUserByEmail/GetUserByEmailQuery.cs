using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<User?>
{
    public string Email { get; set; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}
