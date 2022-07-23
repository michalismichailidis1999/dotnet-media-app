namespace MediaApp.Domain.Aggregates.PostAggregates;

public class PostInteraction : Interaction
{
    public int PostId { get; private set; }

    private PostInteraction()
    {
    }

    public static PostInteraction builder(int postId, Guid userId, InteractionStatus status)
    {
        var validator = new FieldValidator()
            .CheckIfNull(postId, nameof(postId))
            .CheckIfNull(userId, nameof(userId))
            .CheckIfNull(status, nameof(status));

        var createdAt = DateTime.Now;

        var interaction = new PostInteraction
        {
            PostId = postId,
            UserId = userId,
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        };

        if (validator.HasErrors()) interaction.AddErrors(validator.GetErrors());

        return interaction;
    }
}
