using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.SubmitTestResult;

public class SubmitTestResultHandler : IRequestHandler<SubmitTestResultCommand>
{
    private readonly IUnitOfWork _uow;

    public SubmitTestResultHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(SubmitTestResultCommand request, CancellationToken cancellationToken)
    {
        var testAssignment = await _uow.TestAssignmentRepository
            .FindOneAsync(x => x.UserId == request.UserId && x.TestId == request.TestId);

        if (testAssignment is null)
        {
            throw new EntityNotFoundException($"User with {request.UserId} id is not assigned to the test with {request.TestId} id");
        }

        if (testAssignment.TestCompletion is not null)
        {
            throw new EntityConflictException("The user has already passed the test");
        }

        var testData = await _uow.TestRepository.GetByIdAsync(request.TestId);

        if (testData is null)
        {
            throw new EntityNotFoundException($"Test data with {request.UserId} id was not found");
        }

        var testCompletion = request.TestCompletion;

        testCompletion.CorrectAnswersCount = GetCorrectAnswersCount(testData, request.TestCompletion);
        testCompletion.Score = Math.Round(testCompletion.CorrectAnswersCount / (double)testData.Questions.Count, 1, MidpointRounding.AwayFromZero) * 10;
        testCompletion.TestAssignmentId = testAssignment.Id;

        _uow.TestCompletionRepository.Add(testCompletion);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private int GetCorrectAnswersCount(Test testData, TestCompletion testCompletion)
    {
        int correctCount = 0;
        IEnumerable<string> correctAnswers, userAnswers;

        foreach (var question in testData.Questions)
        {
            correctAnswers = question.Answers.Where(x => x.IsCorrect!.Value).Select(x => x.AnswerText).OrderBy(x => x);
            userAnswers = testCompletion.Answers.Where(x => x.QuestionId == question.Id).Select(x => x.AnswerText).OrderBy(x => x);

            if (Enumerable.SequenceEqual(correctAnswers, userAnswers))
            {
                correctCount++;
            }
        }

        return correctCount;
    }
}
