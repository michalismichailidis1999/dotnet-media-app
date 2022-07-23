namespace MediaApp.Api.Dtos.Response.CommentDtos;

public class CommentInteractionResponse
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public InteractionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}