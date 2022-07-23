namespace MediaApp.Infrastructure.Cache;

public class RedisCachingDB : ICachingDB
{
    private readonly IConnectionMultiplexer _redis;

    public RedisCachingDB(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public void CreateEntry<T>(string key, T value)
    {
        if(value is null) throw new ArgumentNullException(nameof(value));

        var db = _redis.GetDatabase();

        string serializedVal;
        if (typeof(T) != typeof(String)) serializedVal = JsonSerializer.Serialize(value);
        else serializedVal = $"{value}";

        db.StringSet(key, serializedVal);
    }

    public T? RetrieveEntry<T>(string key)
    {
        var db = _redis.GetDatabase();

        var value = db.StringGet(key);

        if(!string.IsNullOrEmpty(key) && !value.IsNull) return JsonSerializer.Deserialize<T>(value!);

        return default(T);
    }

    public void DeleteEntry(string key)
    {
        var db = _redis.GetDatabase();

        db.KeyDelete(key);
    }
}