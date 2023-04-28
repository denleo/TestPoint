using TestPoint.Domain;

namespace TestPoint.Application.Tests;

public record TestInformation(Guid Id, string Author, string Name, TestDifficulty Difficulty, int QuestionCount, int EstimatedTime);