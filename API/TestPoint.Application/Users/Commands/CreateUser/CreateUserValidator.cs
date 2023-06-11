using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username).ApplyUsernameRules();

        RuleFor(x => x.Password).ApplyPasswordRules();

        RuleFor(x => x.Email).ApplyEmailRules();

        RuleFor(x => x.FirstName).ApplyFioRules();

        RuleFor(x => x.LastName).ApplyFioRules();
    }
}
