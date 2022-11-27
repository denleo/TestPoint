namespace TestPoint.Domain;

public abstract class Entity<T> : EntityBase
{
    public T Id { get; set; }
}