﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Interfaces.Persistence;

namespace TestPoint.Application.Admins.Queries.CheckAdminLogin;

public class CheckAdminLoginHandler : IRequestHandler<CheckAdminLoginQuery, CheckAdminLoginResponse?>
{
    private readonly IAdminDbContext _adminDbContext;

    public CheckAdminLoginHandler(IAdminDbContext adminDbContext)
    {
        _adminDbContext = adminDbContext;
    }

    public async Task<CheckAdminLoginResponse?> Handle(CheckAdminLoginQuery request, CancellationToken cancellationToken)
    {
        var admin = await _adminDbContext.Administrators
            .Include(x => x.Login)
            .Where(a => a.Login.Username == request.Username)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);


        if (admin is null || !PasswordHelper.VerifyPassword(request.Password, admin.Login.PasswordHash))
        {
            return null;
        }

        return new CheckAdminLoginResponse
        {
            AdminId = admin.Id,
            Username = admin.Login.Username
        };
    }
}