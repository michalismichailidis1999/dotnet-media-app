namespace MediaApp.Api.Controllers.V2;

[ApiVersion("2.0")]
public class UsersController : BaseController
{
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Register([FromBody] RegisterUser user)
    {
        return Ok("Successfull registration in API Version 2");
    }
}