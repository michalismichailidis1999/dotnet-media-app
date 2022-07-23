namespace MediaApp.Domain.Aggregates.PostAggregates;

public abstract class Interaction : BaseAggregate<int>
{
    public Guid UserId { get; protected set; }
    public User User { get; protected set; }
    public InteractionStatus Status { get; protected set; }

    public Interaction UpdateStatus(InteractionStatus status)
    {
        var validator = new FieldValidator().CheckIfNull(status, nameof(status));

        if(validator.HasErrors()) AddErrors(validator.GetErrors());

        Status = status;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }
}

public enum InteractionStatus
{
    LIKE = 0,
    HEART = 1,
    WOW = 2,
    ANGRY = 3,
    SAD = 4
}
