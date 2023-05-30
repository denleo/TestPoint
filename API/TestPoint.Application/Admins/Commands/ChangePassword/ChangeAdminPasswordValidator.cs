using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Admins.Commands.ChangePassword;

public class ChangeAdminPasswordValidator : AbstractValidator<ChangeAdminPasswordCommand>
{
    public ChangeAdminPasswordValidator()
    {
        RuleFor(x => x.OldPassword).ApplyPasswordRules();

        RuleFor(x => x.NewPassword).ApplyPasswordRules();
    }
}
