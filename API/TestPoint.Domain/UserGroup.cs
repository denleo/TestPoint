namespace TestPoint.Domain;

public class UserGroup : AuditableEntity
{
    public string Name { get; set; }

    public Guid AdministratorId { get; set; }
    public ICollection<User> Users { get; set; }
}
