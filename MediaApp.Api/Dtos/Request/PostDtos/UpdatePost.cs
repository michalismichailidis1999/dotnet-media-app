namespace MediaApp.Api.Dtos.Request.PostDtos;

public class UpdatePost
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string Text { get; set; }
}