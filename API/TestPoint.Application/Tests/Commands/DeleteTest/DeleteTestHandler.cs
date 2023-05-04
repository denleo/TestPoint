using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Tests.Commands.DeleteTest;

public class DeleteTestHandler : IRequestHandler<DeleteTestCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteTestHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _uow.TestRepository
            .FindOneAsync(x => x.Id == request.TestId && x.AuthorId == request.AuthorId);

        if (test is null)
        {
            throw new EntityNotFoundException($"Test was not found.");
        }

        _uow.TestRepository.Remove(test);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
