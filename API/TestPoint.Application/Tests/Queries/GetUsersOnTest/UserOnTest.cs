using TestPoint.Application.Users;

namespace TestPoint.Application.Tests.Queries.GetUsersOnTest;

public record UserOnTest(Guid Id, string FirstName, string LastName, string Email, string? Base64Avatar, bool IsPassed) :
              UserInformation(Id, FirstName, LastName, Email, Base64Avatar);
