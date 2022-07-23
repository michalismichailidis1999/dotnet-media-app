namespace MediaApp.Api.Dtos.Request.UserDtos;

public class UpdateEmailUser
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    [EmailAddress]
    public string Email { get; set; }
}