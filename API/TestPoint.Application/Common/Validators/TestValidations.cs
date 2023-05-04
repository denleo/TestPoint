using TestPoint.Application.Common.Exceptions;
using TestPoint.Domain;

namespace TestPoint.Application.Common.Validators;

internal static class TestValidations
{
    public static void ValidateTestConsistency(Test test)
    {
        if (test!.Questions!.Count == 0)
        {
            throw new BadEntityException("Test doesn't contain any questions.");
        }

        foreach (var question in test.Questions)
        {
            ValidateQuestionConsistency(question);
        }
    }

    public static void ValidateQuestionConsistency(Question? question)
    {
        if (question!.Answers!.Count == 0)
        {
            throw new BadEntityException("Question doesn't contain any answers.");
        }

        if (!question.Answers.Any(x => x.IsCorrect))
        {
            throw new BadEntityException("Question should contain at least one correct answer.");
        }

        switch (question.QuestionType)
        {
            case QuestionType.SingleOption:

                if (question.Answers.Where(x => x.IsCorrect).Count() > 1)
                {
                    throw new BadEntityException("Single option question can have only one correct answer.");
                }
                break;

            case QuestionType.TextSubstitution:

                if (question.Answers.Count() != 1)
                {
                    throw new BadEntityException("Text substitution question can have only one choice in total.");
                }
                break;

            default:
                break;
        }
    }
}
