namespace MediaApp.Domain.Exceptions;

internal class InvalidValidatorFieldCheckValue : Exception
{
    public InvalidValidatorFieldCheckValue()
    {
    }

    public InvalidValidatorFieldCheckValue(string? message) : base(message)
    {
    }
}