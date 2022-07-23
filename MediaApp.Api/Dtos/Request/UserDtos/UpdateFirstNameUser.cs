namespace MediaApp.Api.Dtos.Request.UserDtos;

public class UpdateFirstNameUser
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string FirstName { get; set; }
}