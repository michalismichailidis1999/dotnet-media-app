namespace MediaApp.Common.Models;

public abstract class EntityWithErrors<T>
{
    private readonly List<T> _errors = new List<T>();

    public IEnumerable<T> Errors { get { return _errors; } }

    public void AddError(T error)
    {
        _errors.Add(error);
    }

    public void AddErrors(List<T> errors)
    {
        errors.ForEach(error => _errors.Add(error));
    }

    public void ClearErrors() { _errors.Clear(); }

    public bool HasErrors()
    {
        return _errors.Count > 0;
    }

    public List<T> GetErrors() { return _errors.ToList(); }
}
