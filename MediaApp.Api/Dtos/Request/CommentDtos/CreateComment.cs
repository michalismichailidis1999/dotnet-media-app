namespace MediaApp.Api.Dtos.Request.CommentDtos;

public class CreateComment
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public int PostId { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string Text { get; set; }
}
