using MediatR;

namespace TestPoint.Application.Users.Commands.ChangeAvatar;

public class ChangeUserAvatarCommand : IRequest
{
    public Guid UserId { get; set; }
    public string Base64Avatar { get; set; }
}
