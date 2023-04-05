using System.Text.Json.Serialization;

namespace TestPoint.Domain;

public abstract class AuditableEntity : Entity
{
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}