namespace MediaApp.Infrastructure.Cache;

public interface ICachingDB
{
    void CreateEntry<T>(string key, T value);

    T? RetrieveEntry<T>(string key);

    void DeleteEntry(string key);
}