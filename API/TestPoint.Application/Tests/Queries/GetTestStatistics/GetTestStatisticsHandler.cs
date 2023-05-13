using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestStatistics;

public class GetTestStatisticsHandler : IRequestHandler<GetTestStatisticsQuery, TestStatistics?>
{
    private readonly IUnitOfWork _uow;

    public GetTestStatisticsHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<TestStatistics?> Handle(GetTestStatisticsQuery request, CancellationToken cancellationToken)
    {
        var test = await _uow.TestRepository.GetByIdAsync(request.TestId);

        if (test is null)
        {
            throw new EntityNotFoundException($"Test with {request.TestId} id was not found");
        }

        var testCompletionIds = (await _uow.TestAssignmentRepository
            .FilterByAsync(x => x.TestId == test.Id && x.TestCompletion != null))
            .Select(x => x.TestCompletion!.Id);

        var testCompletions = await _uow.TestCompletionRepository
            .FilterByAsync(x => testCompletionIds.Contains(x.Id));

        if (testCompletions.Count() == 0)
        {
            return null;
        }


        var statistics = new TestStatistics() { TestId = test.Id };

        (statistics.AverageScore, statistics.AverageCompletionTime) = GetAverages(testCompletions);

        var questionStatistics = new List<QuestionStatistics>();
        foreach (var question in test.Questions)
        {
            var correctAnswersCount = GetCorrectAnswersCount(question, testCompletions);
            questionStatistics.Add(
                new QuestionStatistics
                {
                    QuestionId = question.Id,
                    QuestionText = question.QuestionText,
                    CorrectAnswersCount = correctAnswersCount,
                    WrongAnswersCount = testCompletions.Count() - correctAnswersCount
                });
        }

        statistics.QuestionStatistics = questionStatistics.ToArray();

        return statistics;
    }

    private int GetCorrectAnswersCount(Question question, IEnumerable<TestCompletion> testCompletions)
    {
        int correctCount = 0;
        IEnumerable<string> correctAnswers, userAnswers;

        correctAnswers = question.Answers.Where(x => x.IsCorrect!.Value).Select(x => x.AnswerText).OrderBy(x => x);

        foreach (var testCompletion in testCompletions)
        {
            userAnswers = testCompletion.Answers.Where(x => x.QuestionId == question.Id).Select(x => x.AnswerText).OrderBy(x => x);

            if (Enumerable.SequenceEqual(correctAnswers, userAnswers))
            {
                correctCount++;
            }
        }

        return correctCount;
    }

    private (double averageScore, double averageCompletionTime) GetAverages(IEnumerable<TestCompletion> testCompletions)
    {
        int passedCount = testCompletions.Count();
        double scoreSum = 0, completionTimeSum = 0;

        foreach (var testCompletion in testCompletions)
        {
            scoreSum += testCompletion.Score;
            completionTimeSum += testCompletion.CompletionTime;
        }

        return (Math.Round(scoreSum / passedCount, 1, MidpointRounding.AwayFromZero),
            Math.Round(completionTimeSum / passedCount, 1, MidpointRounding.AwayFromZero));
    }
}
