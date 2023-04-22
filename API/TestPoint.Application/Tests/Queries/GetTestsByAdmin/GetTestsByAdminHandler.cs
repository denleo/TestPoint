﻿using MediatR;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Tests.Queries.GetTestsByAdmin;

public class GetTestsByAdminHandler : IRequestHandler<GetTestsByAdminQuery, List<Test>>
{
    private readonly IUnitOfWork _uow;

    public GetTestsByAdminHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<List<Test>> Handle(GetTestsByAdminQuery request, CancellationToken cancellationToken)
    {
        var admin = await _uow.AdminRepository.GetByIdAsync(request.AdminId);

        if (admin is null)
        {
            throw new EntityNotFoundException($"Administrator with {request.AdminId} id does not exist");
        }

        var tests = await _uow.TestRepository.FilterByAsync(x => x.AuthorId == admin.Id);
        return tests.ToList();
    }
}
