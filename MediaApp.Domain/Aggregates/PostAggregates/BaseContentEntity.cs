namespace MediaApp.Domain.Aggregates.PostAggregates;

public abstract class BaseContentEntity<T> : BaseAggregate<int> where T : Interaction
{
    protected readonly List<T> _contentInteractions = new List<T>();

    public string Text { get; protected set; }
    public Guid UserId { get; protected set; }
    // public User User { get; protected set; }

    public void UpdateContentText(string text)
    {
        var validator = new FieldValidator()
            .CheckIfNull(text, nameof(text))
            .CheckLength(text, nameof(text), 1, 1000);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        Text = text;
        UpdatedAt = DateTime.Now;
    }
}
