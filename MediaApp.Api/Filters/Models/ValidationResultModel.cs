namespace MediaApp.Api.Filters.Models;

public class ValidationResultModel : ErrorResponse<ValidationError>
{
    public ValidationResultModel(ModelStateDictionary modelState)
    {
        StatusCode = ErrorStatusCodes.BadRequest;
        StatusPhrase = "Bad Request";
        Timestamp = DateTime.Now;

        foreach (var key in modelState.Keys) {
            if (key.Contains("id") || key.Contains("Id")) continue;

            var error = new ValidationError
            {
                Field = key,
                ErrorMessages = modelState[key].Errors.Select(err => err.ErrorMessage).ToList()
            };

            Errors.Add(error);
        }
    }
}

public class ValidationError
{
    public string Field { get; set; }
    public List<string> ErrorMessages { get; set; }
}