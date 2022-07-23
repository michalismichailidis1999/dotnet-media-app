namespace MediaApp.Api.Controllers;

[ApiController]
[Route(ApiRoutes.BaseRoute)]
public abstract class BaseController : ControllerBase
{
    public void CheckIfPathVariableIsValidGuid(string value)
    {
        if (Guid.TryParse(value?.ToString(), out var guid)) return;

        throw new Exception($"The identifier '{guid}' is not a correct GUID format");
    }

    public ErrorResponse<string> GetErrorResponse(
        List<string> errors, 
        ErrorStatusCodes StatusCode = ErrorStatusCodes.BadRequest, 
        string StatusPhrase = "Bad Request"
    )
    {
        var error = new ErrorResponse<string>
        {
            StatusCode = StatusCode,
            StatusPhrase = StatusPhrase,
            Timestamp = DateTime.Now,
        };

        errors.ForEach(err =>
        {
            error.Errors.Add(err);
        });

        return error;
    }
}