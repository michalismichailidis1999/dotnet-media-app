namespace MediaApp.Application.Exceptions;

public class CommentNotFoundException : Exception
{
    public CommentNotFoundException()
    {
    }

    public CommentNotFoundException(string? message) : base(message)
    {
    }
}