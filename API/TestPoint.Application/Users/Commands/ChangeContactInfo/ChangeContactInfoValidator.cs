using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Users.Commands.ChangeContactInfo;

public class ChangeContactInfoValidator : AbstractValidator<ChangeContactInfoCommand>
{
    public ChangeContactInfoValidator()
    {
        RuleFor(x => x.Email).ApplyEmailRules();

        RuleFor(x => x.FirstName).ApplyFioRules();

        RuleFor(x => x.LastName).ApplyFioRules();
    }
}
