namespace MediaApp.Api.Dtos.Request.PostDtos;

public class UpdatePostInteraction
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public InteractionStatus Status { get; set; }
}