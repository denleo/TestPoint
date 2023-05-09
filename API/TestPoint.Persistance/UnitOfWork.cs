using Microsoft.Extensions.Logging;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Application.Interfaces.Persistence.Repositories;
using TestPoint.DAL.Contexts;
using TestPoint.DAL.Repositories;

namespace TestPoint.DAL;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private bool _disposed = false;

    private IAdminRepository _adminRepository = null!;
    private IUserRepository _userRepository = null!;
    private IUserGroupRepository _userGroupRepository = null!;
    private ITestRepository _testRepository = null!;
    private ITestAssignmentRepository _testAssignmentRepository = null!;
    private ITestCompletionRepository _testCompletionRepository = null!;

    public IAdminRepository AdminRepository => _adminRepository ??= new AdminRepository(_context);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public IUserGroupRepository UserGroupRepository => _userGroupRepository ??= new UserGroupRepository(_context);
    public ITestRepository TestRepository => _testRepository ??= new TestRepository(_context);
    public ITestAssignmentRepository TestAssignmentRepository => _testAssignmentRepository ??= new TestAssignmentRepository(_context);
    public ITestCompletionRepository TestCompletionRepository => _testCompletionRepository ??= new TestCompletionRepository(_context);

    public UnitOfWork(AppDbContext appDbContext, ILogger<UnitOfWork> logger)
    {
        _context = appDbContext;
        _logger = logger;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var affected = 0;

        if (_context is null)
        {
            return affected;
        }

        if (_disposed)
        {
            throw new ObjectDisposedException("UnitOfWork");
        }

        try
        {
            affected = await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception? ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new RepositoryException("An error ocсurred while saving data");
        }

        return affected;
    }

    public void Dispose()
    {
        if (_context is null)
        {
            return;
        }

        if (!_disposed)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}