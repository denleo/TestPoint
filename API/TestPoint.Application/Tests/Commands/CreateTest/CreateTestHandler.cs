using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Common.Validators;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Commands.CreateTest;

public class CreateTestHandler : IRequestHandler<CreateTestCommand, Test>
{
    private readonly IUnitOfWork _uow;

    public CreateTestHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Test> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        TestValidations.ValidateTestConsistency(request);

        var testWithTheSameName = await _uow.TestRepository
            .FindOneAsync(x => x.Name == request.Name && x.AuthorId == request.AuthorId);

        if (testWithTheSameName is not null)
        {
            throw new EntityConflictException("Test with the same name already exists.");
        }

        _uow.TestRepository.Add(request);
        await _uow.SaveChangesAsync(cancellationToken);

        return request;
    }
}
