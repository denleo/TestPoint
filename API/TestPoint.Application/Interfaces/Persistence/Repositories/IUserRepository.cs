using TestPoint.Domain;

namespace TestPoint.Application.Interfaces.Persistence.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{

}