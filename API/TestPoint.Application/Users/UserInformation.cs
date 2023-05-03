namespace TestPoint.Application.Users;

public record UserInformationShort(Guid Id, string FirstName, string LastName, string Email);

public record UserInformation(Guid Id, string FirstName, string LastName, string Email, string? Base64Avatar) :
              UserInformationShort(Id, FirstName, LastName, Email);