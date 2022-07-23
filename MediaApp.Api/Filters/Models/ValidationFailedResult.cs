namespace MediaApp.Api.Filters.Models;

public class ValidationFailedResult : JsonResult
{
    public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultModel(modelState))
    {
        StatusCode = 400;
    }
}