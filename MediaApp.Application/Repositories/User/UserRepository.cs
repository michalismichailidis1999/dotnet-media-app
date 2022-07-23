namespace MediaApp.Application.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _ctx;

    public UserRepository(DatabaseContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Create(Domain.Aggregates.UserAggregates.User user)
    {
        _ctx.Users.Add(user);
        await Save();
    }

    public async Task<Domain.Aggregates.UserAggregates.User?> FindByEmail(string email)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Domain.Aggregates.UserAggregates.User?> FindById(Guid id)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Domain.Aggregates.UserAggregates.User?> FindByUsername(string username)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task Update(Domain.Aggregates.UserAggregates.User user)
    {
        _ctx.Users.Update(user);
        await Save();
    }

    public async Task Delete(Domain.Aggregates.UserAggregates.User user)
    {
        _ctx.Remove(user);
        await Save();
    }

    public async Task Save()
    {
        await _ctx.SaveChangesAsync();
    }
}
