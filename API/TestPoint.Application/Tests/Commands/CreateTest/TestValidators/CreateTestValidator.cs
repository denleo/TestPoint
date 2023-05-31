using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Tests.Commands.CreateTest.TestValidators;

public class CreateTestValidator : AbstractValidator<CreateTestCommand>
{
    public CreateTestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(ValidationRulesExtensions.TESTNAME_MIN_LEN)
            .MaximumLength(ValidationRulesExtensions.TESTNAME_MAX_LEN);

        RuleFor(x => x.EstimatedTime).InclusiveBetween(1, 120);

        RuleFor(x => x.Difficulty).IsInEnum();

        RuleFor(x => x.Questions).NotEmpty();

        RuleForEach(x => x.Questions).SetValidator(new TestQuestionValidator());
    }
}
