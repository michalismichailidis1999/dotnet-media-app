namespace MediaApp.Application.CachingModels;

public class PostCachingModel
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}