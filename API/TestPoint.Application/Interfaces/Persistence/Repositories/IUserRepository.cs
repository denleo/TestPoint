﻿using TestPoint.Application.Users;
using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<List<UserInformationShort>> FilterUsersByFIO(string filter);
}