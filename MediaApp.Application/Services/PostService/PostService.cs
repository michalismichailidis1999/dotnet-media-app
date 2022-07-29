namespace MediaApp.Application.Services.PostService;

public class PostService : BaseService, IPostService
{
    protected override string CachingKey => "post:";
    private const string ManyEntriesKey = "posts";
    private const string PostInterationsKey = "postInteractions/post:";

    private readonly IPostRepository _repository;
    private readonly IMessageBusPublisher<MessageBusPostEntity> _publisher;

    public PostService(
        IPostRepository repository,
        ICachingDB cachingDB,
        IMapper mapper,
        IMessageBusPublisher<MessageBusPostEntity> publisher
    ) : base(cachingDB, mapper)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<PostServiceResponse> CreatePost(Post post)
    {
        var postServiceResponse = new PostServiceResponse();

        try
        {
            await _repository.Create(post);

            postServiceResponse.Payload = post;

            _cachingDB.CreateEntry(CachingKey + post.Id, _mapper.Map<PostCachingModel>(post));

            _publisher.Publish(_mapper.Map<MessageBusPostEntity>(post));
        }
        catch (UserNotFoundException e)
        {
            postServiceResponse.AddError(e.Message);
        }

        return postServiceResponse;
    }

    public async Task<List<Post>> GetAllPosts()
    {
        var cachedPosts = _cachingDB.RetrieveEntry<List<PostCachingModel>>(ManyEntriesKey);

        var posts = cachedPosts is not null ? _mapper.Map<List<Post>>(cachedPosts) : await _repository.GetAllPosts();

        if (cachedPosts is null) _cachingDB.CreateEntry(ManyEntriesKey, posts);

        return posts;
    }

    public async Task<PostServiceResponse> GetPostById(int id)
    {
        var postServiceResponse = new PostServiceResponse();

        var cachedPost = _cachingDB.RetrieveEntry<PostCachingModel>(CachingKey + id);

        var post = cachedPost is not null ? _mapper.Map<Post>(cachedPost) : await _repository.FindById(id);

        if (post is not null)
        {
            postServiceResponse.Payload = post;

            if(cachedPost is null) _cachingDB.CreateEntry(CachingKey + post.Id, _mapper.Map<PostCachingModel>(post));
        }
        else postServiceResponse.AddError($"Post with id '{id}' not found");

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> UpdatePost(int id, Guid userId, string Text)
    {
        var postServiceResponse = new PostServiceResponse();

        var post = await _repository.FindById(id);
        if (post is null) postServiceResponse.AddError($"Post with id '{id}' not found");

        if (post is not null && post.UserId != userId)
            postServiceResponse.AddError($"Can't update the post. User id does not match");

        if (!postServiceResponse.HasErrors())
        {
            post!.UpdateText(Text);

            if(post.HasErrors())
            {
                postServiceResponse.AddErrors(post.GetErrors());
                return postServiceResponse;
            }

            await _repository.Update(post);
            _cachingDB.CreateEntry(CachingKey + post.Id, _mapper.Map<PostCachingModel>(post));
            _cachingDB.DeleteEntry(ManyEntriesKey);
        }

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> DeletePost(int id, Guid userId)
    {
        var postServiceResponse = new PostServiceResponse();

        var post = await _repository.FindById(id);
        if (post is null) postServiceResponse.AddError($"Post with id '{id}' not found");

        if (post is not null && post.UserId != userId)
            postServiceResponse.AddError($"Can't update the post. User id does not match");

        if (!postServiceResponse.HasErrors())
        {
            await _repository.Delete(post!);
            _cachingDB.DeleteEntry(CachingKey + post!.Id);
            _cachingDB.DeleteEntry(ManyEntriesKey);
        }

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> GetPostInteractions(int id)
    {
        var postServiceResponse = new PostServiceResponse();

        try
        {
            var cachedInteractions = _cachingDB.RetrieveEntry<List<PostInteractionCachingModel>>(PostInterationsKey + id);

            postServiceResponse.Interactions = 
                cachedInteractions is not null ? _mapper.Map<List<PostInteraction>>(cachedInteractions) : 
                await _repository.GetInteractions(id);

            if (cachedInteractions is null)
                _cachingDB.CreateEntry(
                    PostInterationsKey + id, 
                    _mapper.Map<List<PostInteractionCachingModel>>(postServiceResponse.Interactions)
                );
        }
        catch (PostNotFoundException e)
        {
            postServiceResponse.AddError(e.Message);
        }

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> AddInteractionToPost(PostInteraction interaction)
    {
        var postServiceResponse = new PostServiceResponse();

        try
        {
            await _repository.AddInteraction(interaction);
            postServiceResponse.Interaction = interaction;

            _cachingDB.DeleteEntry(PostInterationsKey + postServiceResponse.Interaction.PostId);
        }
        catch (PostNotFoundException e)
        {
            postServiceResponse.AddError(e.Message);
        }
        catch (UserNotFoundException e)
        {
            postServiceResponse.AddError(e.Message);
        }

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> UpdatePostInteraction(
        int postId, 
        Guid userId, 
        int interactionId, 
        InteractionStatus status
    )
    {
        var postServiceResponse = new PostServiceResponse();

        var existingInteraction = await _repository.GetInteractionById(interactionId);
        if (existingInteraction is null)
            postServiceResponse.AddError($"Post interaction with id '{interactionId}' not found");

        if (existingInteraction is not null && existingInteraction.PostId != postId)
            postServiceResponse.AddError($"Interaction's post id is incorrect");

        if (existingInteraction is not null && existingInteraction.UserId != userId)
            postServiceResponse.AddError($"You can't change the interaction's status. User id does not match");

        if(!postServiceResponse.HasErrors())
        {
            existingInteraction!.UpdateStatus(status);

            if(existingInteraction.HasErrors())
            {
                postServiceResponse.AddErrors(existingInteraction.GetErrors());
                return postServiceResponse;
            }

            await _repository.UpdateInteraction(existingInteraction);
            _cachingDB.DeleteEntry(PostInterationsKey + existingInteraction.PostId);
        }

        return postServiceResponse;
    }

    public async Task<PostServiceResponse> DeletePostInteraction(int postId, Guid userId, int interactionId)
    {
        var postServiceResponse = new PostServiceResponse();

        var existingInteraction = await _repository.GetInteractionById(interactionId);
        if (existingInteraction is null)
            postServiceResponse.AddError($"Post interaction with id '{interactionId}' not found");

        if (existingInteraction is not null && existingInteraction.PostId != postId)
            postServiceResponse.AddError($"Interaction's post id is incorrect");

        if (existingInteraction is not null && existingInteraction.UserId != userId)
            postServiceResponse.AddError($"You can't delete this interaction. User id does not match");

        if (!postServiceResponse.HasErrors())
        {
            await _repository.DeleteInteraction(existingInteraction!);
            _cachingDB.DeleteEntry(PostInterationsKey + existingInteraction!.PostId);
        }

        return postServiceResponse;
    }
}