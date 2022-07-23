using MediaApp.Domain.Aggregates.PostAggregates;

namespace MediaApp.Domain.Aggregates.UserAggregates;

public class User : BaseAggregate<Guid>
{
    private User() { }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public static User builder(string firstName, string lastName, string username, string email, string password)
    {
        var validator = new FieldValidator()
            .CheckIfNull(firstName, nameof(firstName))
            .CheckLength(firstName, nameof(firstName), 2, 20)
            .CheckIfNull(lastName, nameof(lastName))
            .CheckLength(lastName, nameof(lastName), 2, 20)
            .CheckIfNull(username, nameof(username))
            .CheckLength(username, nameof(username), 3, 30)
            .CheckIfNull(email, nameof(email))
            .CheckLength(email, nameof(email), 5, 200)
            .CheckIfNull(password, nameof(password))
            .CheckLength(password, nameof(password), 4);

        var createdAt = DateTime.Now;

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            Email = email,
            Password = password,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        if (validator.HasErrors()) user.AddErrors(validator.GetErrors());

        return user;
    }

    public User UpdateFirstName(string firstName)
    {
        var validator = new FieldValidator()
            .CheckIfNull(firstName, nameof(firstName))
            .CheckLength(firstName, nameof(firstName), 2, 20);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        FirstName = firstName;
        UpdatedAt = DateTime.Now;

        return this;
    }

    public User UpdateLastName(string lastName)
    {
        var validator = new FieldValidator()
            .CheckIfNull(lastName, nameof(lastName))
            .CheckLength(lastName, nameof(lastName), 2, 20);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        LastName = lastName;
        UpdatedAt = DateTime.Now;

        return this;
    }

    public User UpdateUsername(string username)
    {
        var validator = new FieldValidator()
            .CheckIfNull(username, nameof(username))
            .CheckLength(username, nameof(username), 3, 20);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        Username = username;
        UpdatedAt = DateTime.Now;

        return this;
    }

    public User UpdateEmail(string email)
    {
        var validator = new FieldValidator()
            .CheckIfNull(email, nameof(email))
            .CheckLength(email, nameof(email), 5, 200);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        Email = email;
        UpdatedAt = DateTime.Now;

        return this;
    }

    public User UpdatePassword(string password)
    {
        var validator = new FieldValidator()
            .CheckIfNull(password, nameof(password))
            .CheckLength(password, nameof(password), 4);

        if (validator.HasErrors()) AddErrors(validator.GetErrors());

        Password = password;
        UpdatedAt = DateTime.Now;

        return this;
    }
}
