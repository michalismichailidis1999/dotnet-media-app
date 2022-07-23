namespace MediaApp.Application.Repositories.Comment;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _ctx;

    public CommentRepository(DatabaseContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Domain.Aggregates.PostAggregates.Comment?> FindById(int id)
    {
        return await _ctx.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Domain.Aggregates.PostAggregates.Comment>> GetAllPostComments(int postId)
    {
        var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if(post is null) throw new PostNotFoundException($"Post with id '{postId}' not found");

        return await _ctx.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task Create(Domain.Aggregates.PostAggregates.Comment comment)
    {
        var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == comment.PostId);
        if (post is null) throw new PostNotFoundException($"Post with id '{comment.PostId}' not found");

        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == comment.UserId);
        if (user is null) throw new UserNotFoundException($"User with id '{comment.UserId}' not found");

        _ctx.Comments.Add(comment);
        await Save();
    }

    public async Task Update(Domain.Aggregates.PostAggregates.Comment comment)
    {
        _ctx.Comments.Update(comment);
        await Save();
    }

    public async Task Delete(Domain.Aggregates.PostAggregates.Comment comment)
    {
        _ctx.Comments.Remove(comment);
        await Save();
    }

    public async Task<List<CommentInteraction>> GetInteractions(int commentId)
    {
        var comment = await _ctx.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment is null) throw new PostNotFoundException($"Comment with id '{commentId}' not found");

        return await _ctx.CommentInteractions.Where(ci => ci.CommentId == commentId).ToListAsync();
    }

    public async Task<CommentInteraction?> GetInteractionById(int id)
    {
        return await _ctx.CommentInteractions.FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public async Task AddInteraction(CommentInteraction interaction)
    {
        var comment = await _ctx.Comments.FirstOrDefaultAsync(c => c.Id == interaction.CommentId);
        if (comment is null) throw new CommentNotFoundException($"Comment with id '{interaction.CommentId}' not found");

        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == interaction.UserId);
        if (user is null) throw new UserNotFoundException($"User with id '{interaction.UserId}' not found");

        _ctx.CommentInteractions.Add(interaction);
        await Save();
    }

    public async Task UpdateInteraction(CommentInteraction interaction)
    {
        _ctx.CommentInteractions.Update(interaction);
        await Save();
    }

    public async Task DeleteInteraction(CommentInteraction interaction)
    {
        _ctx.CommentInteractions.Remove(interaction);
        await Save();
    }

    public async Task Save()
    {
        await _ctx.SaveChangesAsync();
    }
}
