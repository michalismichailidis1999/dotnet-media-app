namespace MediaApp.Domain.Aggregates.PostAggregates;

public class Post : BaseContentEntity<PostInteraction>
{
    private readonly List<Comment> _comments = new List<Comment>();

    public IEnumerable<Comment> Comments { get { return _comments; } }
    public IEnumerable<PostInteraction> PostInteractions { get { return _contentInteractions; } }

    private Post()
    {
    }

    public static Post builder(Guid userId, string text)
    {
        var validator = new FieldValidator()
            .CheckIfNull(userId, nameof(userId))
            .CheckIfNull(text, nameof(text))
            .CheckLength(text, nameof(text), 1, 1000);

        var createdAt = DateTime.Now;

        var post = new Post()
        {
            UserId = userId,
            Text = text,
            CreatedAt = createdAt,
            UpdatedAt = createdAt,
        };

        if (validator.HasErrors()) post.AddErrors(validator.GetErrors());

        return post;
    }

    public Post UpdateText(string text)
    {
        UpdateContentText(text);

        return this;
    }

    public int GetCommentsCount() { return _comments.Count; }

    public void AddComment(Comment comment) { _comments.Add(comment); }

    public void RemoveComment(Comment comment) { _comments.Remove(comment); }

    public void UpdateComment(Comment comment)
    {
        var existingComment = _comments.FirstOrDefault(c => c.Id == comment.Id);

        if (existingComment is not null) existingComment.UpdateText(comment.Text);
    }

    public List<Comment> GetComments() { return _comments.ToList(); }
}
