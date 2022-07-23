namespace MediaApp.Api.Dtos.Request.PostDtos;

public class AddPostInteraction
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public InteractionStatus Status { get; set; }
}
