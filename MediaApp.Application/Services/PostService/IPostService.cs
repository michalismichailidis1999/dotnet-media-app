namespace MediaApp.Application.Services.PostService;

public interface IPostService
{
    Task<PostServiceResponse> CreatePost(Post post);
    Task<List<Post>> GetAllPosts();
    Task<PostServiceResponse> GetPostById(int id);
    Task<PostServiceResponse> UpdatePost(int id, Guid userId, string Text);
    Task<PostServiceResponse> DeletePost(int id, Guid userId);
    Task<PostServiceResponse> GetPostInteractions(int postId);
    Task<PostServiceResponse> AddInteractionToPost(PostInteraction interaction);
    Task<PostServiceResponse> UpdatePostInteraction(int postId, Guid userId, int interactionId, InteractionStatus status);
    Task<PostServiceResponse> DeletePostInteraction(int postId, Guid userId, int interactionId);
}