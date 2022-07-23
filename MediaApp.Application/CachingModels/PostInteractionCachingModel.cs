namespace MediaApp.Application.CachingModels;

public class PostInteractionCachingModel
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Guid UserId { get; set; }
    public InteractionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}