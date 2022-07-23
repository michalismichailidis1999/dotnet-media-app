namespace MediaApp.Api.Dtos.Request.UserDtos;

public class UpdateLastNameUser
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string LastName { get; set; }
}