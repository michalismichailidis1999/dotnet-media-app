namespace MediaApp.Application.Repositories.Comment;

public interface ICommentRepository : IContentRepository<Domain.Aggregates.PostAggregates.Comment, int, CommentInteraction, int>
{
    Task<List<Domain.Aggregates.PostAggregates.Comment>> GetAllPostComments(int postId);
}
