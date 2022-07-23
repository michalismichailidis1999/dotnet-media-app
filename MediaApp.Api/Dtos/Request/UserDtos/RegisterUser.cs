namespace MediaApp.Api.Dtos.Request.UserDtos;

public class RegisterUser
{
    [Required(ErrorMessage = "Firstname is required")]
    [MinLength(2, ErrorMessage = "Firstname must be at least 2 characters")]
    [MaxLength(20, ErrorMessage = "Firstname must be at most 20 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    [MinLength(2, ErrorMessage = "Lastname must be at least 2 characters")]
    [MaxLength(20, ErrorMessage = "Lastname must be at most 20 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    [MaxLength(30, ErrorMessage = "Username must be at most 30 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MinLength(5, ErrorMessage = "Email must be at least 5 characters")]
    [MaxLength(200, ErrorMessage = "Email must be at most 200 characters")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
    public string Password { get; set; }
}
