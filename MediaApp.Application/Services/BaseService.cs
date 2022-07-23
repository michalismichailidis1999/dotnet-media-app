namespace MediaApp.Application.Services;

public abstract class BaseService
{
    protected abstract string CachingKey { get; }

    protected readonly ICachingDB _cachingDB;
    protected readonly IMapper _mapper;

    protected BaseService(ICachingDB cachingDB, IMapper mapper)
    {
        _cachingDB = cachingDB;
        _mapper = mapper;
    }
}