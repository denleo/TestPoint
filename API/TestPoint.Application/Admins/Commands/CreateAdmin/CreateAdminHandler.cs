using MediatR;
using TestPoint.Application.Common.Encryption;
using TestPoint.Application.Common.Exceptions;
using TestPoint.Application.Interfaces.Persistence;
using TestPoint.Domain;

namespace TestPoint.Application.Admins.Commands.CreateAdmin;

public class CreateAdminHandler : IRequestHandler<CreateAdminCommand, CreateAdminResponse>
{
    private readonly IUnitOfWork _uow;

    public CreateAdminHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<CreateAdminResponse> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var adminWithSameLogin = await _uow.AdminRepository
            .FindOneAsync(x => x.Login.Username == request.Username);

        if (adminWithSameLogin is not null)
        {
            throw new EntityConflictException("Username is already taken.");
        }

        var tempPassword = PasswordHelper.CreateRandomPassword();

        var newAdmin = new Administrator
        {
            Login = new SystemLogin
            {
                LoginType = LoginType.Administrator,
                Username = request.Username,
                PasswordHash = PasswordHelper.ComputeHash(tempPassword),
                PasswordReseted = true,
                RegistryDate = DateTime.Now
            }
        };

        _uow.AdminRepository.Add(newAdmin);
        await _uow.SaveChangesAsync(cancellationToken);

        return new CreateAdminResponse
        {
            AdminId = newAdmin.Id,
            TempPassword = tempPassword
        };
    }
}