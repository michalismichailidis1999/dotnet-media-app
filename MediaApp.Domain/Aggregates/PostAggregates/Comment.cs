namespace MediaApp.Domain.Aggregates.PostAggregates;

public class Comment : BaseContentEntity<CommentInteraction>
{
    public IEnumerable<CommentInteraction> CommentInteractions { get { return _contentInteractions; } }
    public int PostId { get; private set; }

    private Comment()
    {
    }

    public static Comment builder(int postId, Guid userId, string text)
    {
        var validator = new FieldValidator()
            .CheckIfNull(postId, nameof(postId))
            .CheckIfNull(userId, nameof(userId))
            .CheckIfNull(text, nameof(text))
            .CheckLength(text, nameof(text), 1, 1000);

        var createdAt = DateTime.Now;

        var comment = new Comment()
        {
            PostId = postId,
            UserId = userId,
            Text = text,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        if (validator.HasErrors()) comment.AddErrors(validator.GetErrors());

        return comment;
    }

    public Comment UpdateText(string text)
    {
        UpdateContentText(text);

        return this;
    }
}