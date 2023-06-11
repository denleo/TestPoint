namespace TestPoint.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserResponse
{
    public Guid UserId { get; set; }

    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool PasswordReseted { get; set; }
    public bool GoogleAuthEnabled { get; set; }
    public DateTime RegistryDate { get; set; }
    public string? Base64Avatar { get; set; }
}