using MediatR;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestById;

public class GetTestByIdHandler : IRequestHandler<GetTestByIdQuery, Test?>
{
    private readonly IUnitOfWork _uow;

    public GetTestByIdHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Test?> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _uow.TestRepository.GetByIdAsync(request.TestId);
    }
}
