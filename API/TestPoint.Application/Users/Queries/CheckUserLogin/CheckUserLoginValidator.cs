using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Queries.CheckUserLogin;

public class CheckUserLoginValidator : AbstractValidator<CheckUserLoginQuery>
{
    public CheckUserLoginValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .MinimumLength(ValidationRulesExtensions.USERNAME_MIN_LEN)
            .MaximumLength(ValidationRulesExtensions.EMAIL_MAX_LEN);

        RuleFor(x => x.Password).ApplyPasswordRules();
    }
}
