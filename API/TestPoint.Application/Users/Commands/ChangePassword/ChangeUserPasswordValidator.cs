using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Commands.ChangePassword;

public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(x => x.OldPassword).ApplyPasswordRules();

        RuleFor(x => x.NewPassword).ApplyPasswordRules();
    }
}
