namespace MediaApp.Application.Services.CommentService;

public interface ICommentService
{
    Task<CommentServiceResponse> GetAllPostComments(int postId);
    Task<CommentServiceResponse> CreateComment(Comment comment);
    Task<CommentServiceResponse> UpdateComment(int postId, Guid userId, int commentId, string text);
    Task<CommentServiceResponse> DeleteComment(int postId, Guid userId, int commentId);
    Task<CommentServiceResponse> GetCommentInteractions(int commentId);
    Task<CommentServiceResponse> AddCommentInteraction(CommentInteraction interaction);
    Task<CommentServiceResponse> UpdateCommentInteraction(int commentId, Guid userId, int interactionId, InteractionStatus status);
    Task<CommentServiceResponse> DeleteCommentInteraction(int commentId, Guid userId, int interactionId);
}