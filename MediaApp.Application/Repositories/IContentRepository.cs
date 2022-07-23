namespace MediaApp.Application.Repositories;

// T -> Entity type (Post or Comment)
// K -> Entity T id
// R -> Interaction entity type
// L -> Interaction id
public interface IContentRepository<T, K, R, L> : IRepository<T, K> where R : Interaction
{
    Task<R?> GetInteractionById(L id);
    Task<List<R>> GetInteractions(K id);
    Task AddInteraction(R interaction);
    Task UpdateInteraction(R interaction);
    Task DeleteInteraction(R interaction);
}