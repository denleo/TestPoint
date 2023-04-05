using System.Text.Json.Serialization;

namespace TestPoint.Domain;

public abstract class Entity
{
    [JsonPropertyOrder(-99)]
    public Guid Id { get; set; } = Guid.NewGuid();
}