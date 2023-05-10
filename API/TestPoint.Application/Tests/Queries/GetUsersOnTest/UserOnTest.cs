using TestPoint.Application.Users;

namespace TestPoint.Application.Tests.Queries.GetUsersOnTest;

public record UserOnTest(Guid Id, string FirstName, string LastName, string Email, bool isPassed) :
              UserInformationShort(Id, FirstName, LastName, Email);
