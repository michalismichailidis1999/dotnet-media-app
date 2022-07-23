namespace MediaApp.Application.Repositories.User;

public interface IUserRepository : IRepository<Domain.Aggregates.UserAggregates.User, Guid>
{
    Task<Domain.Aggregates.UserAggregates.User?> FindByEmail(string email);
    Task<Domain.Aggregates.UserAggregates.User?> FindByUsername(string username);
}
