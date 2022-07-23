namespace MediaApp.Api.Dtos.Response.PostDtos;

public class PostInteractionResponse
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public InteractionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
