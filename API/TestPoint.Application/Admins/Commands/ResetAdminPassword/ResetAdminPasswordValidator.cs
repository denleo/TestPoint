using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Admins.Commands.ResetAdminPassword;

public class ResetAdminPasswordValidator : AbstractValidator<ResetAdminPasswordCommand>
{
    public ResetAdminPasswordValidator()
    {
        RuleFor(x => x.Username).ApplyUsernameRules();
    }
}
