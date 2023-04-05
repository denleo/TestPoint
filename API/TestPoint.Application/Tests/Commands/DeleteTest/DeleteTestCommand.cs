using MediatR;

namespace TestPoint.Application.Tests.Commands.DeleteTest;

public class DeleteTestCommand: IRequest
{
    public Guid TestId { get; set; }
    public Guid AuthorId { get; set; }
}
