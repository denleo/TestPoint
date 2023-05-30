using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginValidator : AbstractValidator<CheckAdminLoginQuery>
{
    public CheckAdminLoginValidator()
    {
        RuleFor(x => x.Username).ApplyUsernameRules();

        RuleFor(x => x.Password).ApplyPasswordRules();
    }
}
