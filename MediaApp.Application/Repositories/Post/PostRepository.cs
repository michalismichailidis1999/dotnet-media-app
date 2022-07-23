using System.Linq;

namespace MediaApp.Application.Repositories.Post;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext _ctx;

    public PostRepository(DatabaseContext ctx)
    {
        _ctx = ctx;
    }

    public async Task Create(Domain.Aggregates.PostAggregates.Post post)
    {
        var existingUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == post.UserId);

        if (existingUser is null) throw new UserNotFoundException($"User with id '{post.UserId}' not found");

        _ctx.Posts.Add(post);
        await Save();
    }

    public async Task<Domain.Aggregates.PostAggregates.Post?> FindById(int id)
    {
        return await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Domain.Aggregates.PostAggregates.Post>> GetAllPosts()
    {
        return await _ctx.Posts.ToListAsync();
    }

    public async Task Update(Domain.Aggregates.PostAggregates.Post obj)
    {
        _ctx.Posts.Update(obj);
        await Save();
    }

    public async Task Delete(Domain.Aggregates.PostAggregates.Post obj)
    {
        _ctx.Remove(obj);
        await Save();
    }

    public async Task<List<PostInteraction>> GetInteractions(int id)
    {
        var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == id);

        if (post is null) throw new PostNotFoundException($"Post with id '{id}' not found");

        return await _ctx.PostInteractions.Where(pi => pi.PostId == id).ToListAsync();
    }

    public async Task AddInteraction(PostInteraction interaction)
    {
        var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == interaction.PostId);
        if (post is null) throw new PostNotFoundException($"Post with id '{interaction.PostId}' not found");

        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == interaction.UserId);
        if (user is null) throw new UserNotFoundException($"User with id '{interaction.UserId}' not found");

        _ctx.PostInteractions.Add(interaction);
        await Save();
    }

    public async Task UpdateInteraction(PostInteraction interaction)
    {
        _ctx.PostInteractions.Update(interaction);
        await Save();
    }

    public async Task DeleteInteraction(PostInteraction interaction)
    {
        _ctx.PostInteractions.Remove(interaction);
        await Save();
    }

    public async Task<PostInteraction?> GetInteractionById(int id)
    {
        return await _ctx.PostInteractions.FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task Save()
    {
        await _ctx.SaveChangesAsync();
    }
}