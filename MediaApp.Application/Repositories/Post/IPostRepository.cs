namespace MediaApp.Application.Repositories.Post;

public interface IPostRepository : IContentRepository<Domain.Aggregates.PostAggregates.Post, int, PostInteraction, int>
{ 
    Task<List<Domain.Aggregates.PostAggregates.Post>> GetAllPosts();
}