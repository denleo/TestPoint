using FluentValidation;
using TestPoint.Application.Pipeline.Common;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.CreateTest.TestValidators;

public class TestAnswerValidator : AbstractValidator<Answer>
{
    public TestAnswerValidator()
    {
        RuleFor(x => x.AnswerText)
            .NotEmpty()
            .MaximumLength(ValidationRulesExtensions.TESTANSWER_MAX_LEN);

        RuleFor(x => x.IsCorrect).Must(x => x.HasValue);
    }
}
