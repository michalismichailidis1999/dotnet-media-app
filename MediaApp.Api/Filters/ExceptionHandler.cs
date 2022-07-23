namespace MediaApp.Api.Filters;

public class ExceptionHandler : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var apiError = new ErrorResponse<string>
        {
            StatusCode = ErrorStatusCodes.InternalServerError,
            StatusPhrase = "Internal Server Error",
            Timestamp = DateTime.Now
        };

        apiError.Errors.Add(context.Exception.Message);

        context.Result = new JsonResult(apiError)
        {
            StatusCode = 500
        };
    }
}