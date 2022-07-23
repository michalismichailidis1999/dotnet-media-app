namespace MediaApp.Application.CachingModels;

public class CommentInteractionCachingModel
{
    public int Id { get; set; }
    public int Commentid { get; set; }
    public Guid UserId { get; set; }
    public InteractionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}