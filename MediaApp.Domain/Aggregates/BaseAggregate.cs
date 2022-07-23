namespace MediaApp.Domain.Aggregates;

public abstract class BaseAggregate<T> : EntityWithErrors<string>
{
    public T Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
}