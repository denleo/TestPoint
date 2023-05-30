using FluentValidation;
using TestPoint.Application.Pipeline.Common;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.CreateTest.TestValidators;

public class TestQuestionValidator : AbstractValidator<Question>
{
    public TestQuestionValidator()
    {
        RuleFor(x => x.QuestionText)
            .NotEmpty()
            .MaximumLength(ValidationRulesExtensions.TESTQUESTION_MAX_LEN);

        RuleFor(x => x.QuestionType).IsInEnum();

        RuleForEach(x => x.Answers).SetValidator(new TestAnswerValidator());

        RuleFor(question => question.Answers).NotEmpty();

        RuleFor(question => question.Answers)
            .Must(answers => answers.Any(answer => answer.IsCorrect!.Value))
            .WithMessage("Question should contain at least one correct answer");

        RuleFor(question => question.Answers)
            .Must(answers => answers.Where(answer => answer.IsCorrect!.Value).Count() == 1)
            .When(question => question.QuestionType == QuestionType.SingleOption)
            .WithMessage("Single option question can have only one correct answer.");

        RuleFor(question => question.Answers)
            .Must(answers => answers.Count() == 1)
            .When(question => question.QuestionType == QuestionType.TextSubstitution)
            .WithMessage("Text substitution question can have only one choice in total.");

        RuleFor(question => question.Answers)
            .Must(answers => answers.GroupBy(a => a.AnswerText).All(g => g.Count() == 1))
            .WithMessage("Question can't contain duplicate answers");
    }
}
