namespace MediaApp.Api.Dtos.Request.UserDtos;

public class UpdateUsernameUser
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Username { get; set; }
}