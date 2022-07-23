namespace MediaApp.Api.Dtos.Request.CommentDtos;

public class UpdateCommentInteraction
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public InteractionStatus Status { get; set; }
}