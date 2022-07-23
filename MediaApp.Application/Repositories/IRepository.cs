namespace MediaApp.Application.Repositories;

public interface IRepository<T, K>
{
    Task Save();
    Task Create(T obj);
    Task Update(T obj);
    Task Delete(T obj);
    Task<T?> FindById(K id);
}