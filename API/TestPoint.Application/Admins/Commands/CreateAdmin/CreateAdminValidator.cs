using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Admins.Commands.CreateAdmin;

public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminValidator()
    {
        RuleFor(x => x.Username).ApplyUsernameRules();
    }
}
