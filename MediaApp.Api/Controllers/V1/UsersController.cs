namespace MediaApp.Api.Controllers.V1;

[ApiVersion("1.0")]
public class UsersController : BaseController
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UsersController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost(ApiRoutes.UserRoutes.Register)]
    [ValidateModel]
    public async Task<IActionResult> Register([FromBody] RegisterUser user)
    {
        var newUser = Domain.Aggregates.UserAggregates.User
            .builder(
                user.FirstName,
                user.LastName,
                user.Username,
                user.Email,
                user.Password
            );

        if (newUser.HasErrors()) return BadRequest(GetErrorResponse(newUser.Errors.ToList()));

        var result = await _service.Register(newUser);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        var response = _mapper.Map<AuthUserResponse>(result.Payload);

        if (result.Token is null) return StatusCode(500, "Something went wrong. The authentication token wasn't created");
        response.Token = result.Token!;

        return Ok(response);
    }

    [HttpPost(ApiRoutes.UserRoutes.Login)]
    [ValidateModel]
    public async Task<IActionResult> Login([FromBody] LoginUser user)
    {
        var result = await _service.Login(_mapper.Map<User>(user));

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        var response = _mapper.Map<AuthUserResponse>(result.Payload);

        if (result.Token is null) return StatusCode(500, "Something went wrong. The authentication token wasn't created");
        response.Token = result.Token!;

        return Ok(response);
    }

    [Authorize]
    [HttpPut(ApiRoutes.UserRoutes.UpdateFirstName)]
    [ValidateModel]
    public async Task<IActionResult> UpdateFirstName([FromBody] UpdateFirstNameUser updateFirstNameUser)
    {
        var result = await _service.UpdateFirstName(updateFirstNameUser.Id, updateFirstNameUser.FirstName);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        return Ok("User's first name updated successfully");
    }

    [Authorize]
    [HttpPut(ApiRoutes.UserRoutes.UpdateLastName)]
    [ValidateModel]
    public async Task<IActionResult> UpdateFirstName([FromBody] UpdateLastNameUser updateLastNameUser)
    {
        var result = await _service.UpdateLastName(updateLastNameUser.Id, updateLastNameUser.LastName);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        return Ok("User's last name updated successfully");
    }

    [Authorize]
    [HttpPut(ApiRoutes.UserRoutes.UpdateEmail)]
    [ValidateModel]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailUser updateEmailUser)
    {
        var result = await _service.UpdateEmail(updateEmailUser.Id, updateEmailUser.Email);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        return Ok("User's email updated successfully");
    }

    [Authorize]
    [HttpPut(ApiRoutes.UserRoutes.UpdateUsername)]
    [ValidateModel]
    public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameUser updateUsernameUser)
    {
        var result = await _service.UpdateUsername(updateUsernameUser.Id, updateUsernameUser.Username);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        return Ok("User's username updated successfully");
    }

    [Authorize]
    [HttpDelete(ApiRoutes.UserRoutes.UserId)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        CheckIfPathVariableIsValidGuid(id);

        Guid userId = Guid.Parse(id);

        var result = await _service.DeleteUser(userId);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.Errors.ToList()));

        return Ok("User deleted successfully");
    }
}
