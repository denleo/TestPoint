using TestPoint.Application.Users;

namespace TestPoint.Application.Tests.Queries.GetUsersOnTest;

public record UserOnTest(Guid Id, string FirstName, string LastName, string Email, string? base64Avatar, bool isPassed) :
              UserInformation(Id, FirstName, LastName, Email, base64Avatar);
