using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        When(x => x.IsGoogleAccount, () =>
        {
            RuleFor(x => x.Username).Null();
            RuleFor(x => x.Password).Null();
            RuleFor(x => x.GoogleAvatar).NotEmpty();
        }).Otherwise(() =>
        {
            RuleFor(x => x.Username!).ApplyUsernameRules();
            RuleFor(x => x.Password!).ApplyPasswordRules();
            RuleFor(x => x.GoogleAvatar).Null();
        });

        RuleFor(x => x.Email).ApplyEmailRules();

        RuleFor(x => x.FirstName).ApplyFioRules();

        RuleFor(x => x.LastName).ApplyFioRules();
    }
}
