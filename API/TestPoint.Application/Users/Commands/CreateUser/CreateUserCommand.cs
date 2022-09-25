using MediatR;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}