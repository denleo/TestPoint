namespace TestPoint.Application.Users;

public record UserInformation(Guid Id, string FirstName, string LastName, string Email, string? Base64Avatar);