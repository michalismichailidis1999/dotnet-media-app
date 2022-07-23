namespace MediaApp.Domain.Aggregates.PostAggregates;

public class CommentInteraction : Interaction
{
    public int CommentId { get; private set; }
    private CommentInteraction()
    {
    }

    public static CommentInteraction builder(int commentId, Guid userId, InteractionStatus status)
    {
        var validator = new FieldValidator()
            .CheckIfNull(commentId, nameof(commentId))
            .CheckIfNull(userId, nameof(userId))
            .CheckIfNull(status, nameof(status));

        var createdAt = DateTime.Now;

        var interaction = new CommentInteraction
        {
            CommentId = commentId,
            UserId = userId,
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        };

        if (validator.HasErrors()) interaction.AddErrors(validator.GetErrors());

        return interaction;
    }
}
