namespace MediaApp.Domain.Validators;

internal class FieldValidator : EntityWithErrors<string>
{
    public FieldValidator CheckIfNull<T>(T field, string fieldName)
    {
        if (field is null)
        {
            AddError($"Field '{fieldName}' must not be null");
        }

        if (typeof(T) == typeof(String) && field is not null && field.Equals(""))
        {
            AddError($"Field '{fieldName}' must not be an empty string");
        }

        return this;
    }

    public FieldValidator CheckLength(string field, string fieldName, int min = 1, int? max = null)
    {
        if (min <= 0)
        {
            throw new InvalidValidatorFieldCheckValue($"min must be >= 0, you inserted: min={min}");
        }

        if (max < min)
        {
            throw new InvalidValidatorFieldCheckValue($"max must be greater or equal to min value, you inserted min={min} & max={max}");
        }

        if (field.Length < min)
        {
            AddError($"Field '{fieldName}' must be at least {min} characters");
        }

        if (max is not null && field.Length > max)
        {
            AddError($"Field '{fieldName}' must be less than or equal to {max} characters");
        }

        return this;
    }
}