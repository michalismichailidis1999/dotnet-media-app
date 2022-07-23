namespace MediaApp.Api.Dtos.Response.CommentDtos;

public class CommentResponse
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}