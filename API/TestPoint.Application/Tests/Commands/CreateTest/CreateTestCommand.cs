using MediatR;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.CreateTest;

public class CreateTestCommand : Test, IRequest<Test>
{
}
