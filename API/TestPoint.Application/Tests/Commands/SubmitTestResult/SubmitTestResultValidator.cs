using FluentValidation;
using TestPoint.Application.Pipeline.Common;

namespace TestPoint.Application.Tests.Commands.SubmitTestResult;
public class SubmitTestResultValidator : AbstractValidator<SubmitTestResultCommand>
{
    public SubmitTestResultValidator()
    {
        RuleFor(x => x.TestCompletion.CompletionTime).GreaterThanOrEqualTo(1);

        RuleFor(x => x.TestCompletion.Answers).NotEmpty();

        RuleForEach(x => x.TestCompletion.Answers)
            .ChildRules(x => x.RuleFor(x => x.AnswerText)
                              .NotEmpty()
                              .MaximumLength(ValidationRulesExtensions.TESTANSWER_MAX_LEN));
    }
}
