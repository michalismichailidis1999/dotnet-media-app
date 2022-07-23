namespace MediaApp.Api.Dtos.Response.ErrorDtos;

public class ErrorResponse<T>
{
    public ErrorStatusCodes StatusCode { get; set; }
    public string StatusPhrase { get; set; }
    public List<T> Errors { get; } = new List<T>();
    public DateTime Timestamp { get; set; }
}
