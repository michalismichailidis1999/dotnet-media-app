namespace MediaApp.Application.Services.CommentService;

public class CommentService : BaseService, ICommentService
{
    private readonly ICommentRepository _repository;

    protected override string CachingKey => "comment:";
    private const string ManyEntriesKey = "comments";
    private const string CommentInterationsKey = "commentInteractions/comment:";

    public CommentService(
        ICommentRepository repository,
        ICachingDB cachingDB,
        IMapper mapper
    ) : base(cachingDB, mapper)
    {
        _repository = repository;
    }

    public async Task<CommentServiceResponse> GetAllPostComments(int postId)
    {
        var commentServiceResponse = new CommentServiceResponse();

        try
        {
            var cachedComments = _cachingDB.RetrieveEntry<List<CommentCachingModel>>(ManyEntriesKey);

            commentServiceResponse.Comments = 
                commentServiceResponse is not null ? 
                _mapper.Map<List<Comment>>(cachedComments) : 
                await _repository.GetAllPostComments(postId);

            if (cachedComments is null)
                _cachingDB.CreateEntry(
                    ManyEntriesKey, 
                    _mapper.Map<List<CommentCachingModel>>(commentServiceResponse!.Comments)
                );
        }
        catch (PostNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }

        return commentServiceResponse!;
    }

    public async Task<CommentServiceResponse> CreateComment(Comment comment)
    {
        var commentServiceResponse = new CommentServiceResponse();

        try
        {
            await _repository.Create(comment);
            commentServiceResponse.Payload = comment;
            _cachingDB.CreateEntry(CachingKey + comment.Id, _mapper.Map<CommentCachingModel>(comment));
        }
        catch (PostNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }
        catch (UserNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> UpdateComment(int postId, Guid userId, int commentId, string text)
    {
        var commentServiceResponse = new CommentServiceResponse();

        var cachedComment = _cachingDB.RetrieveEntry<CommentCachingModel>(CachingKey + commentId);

        var comment = cachedComment is not null ? 
            _mapper.Map<Comment>(cachedComment) : 
            await _repository.FindById(commentId);
        if (comment is null) commentServiceResponse.AddError($"Comment with id '{commentId}' not found");

        if(comment is not null && postId != comment.PostId)
            commentServiceResponse.AddError($"You can't change the comment. Post id does not match");

        if(comment is not null && userId != comment.UserId)
            commentServiceResponse.AddError($"You can't change the comment. User id does not match");

        if (!commentServiceResponse.HasErrors())
        {
            comment!.UpdateText(text);

            if(comment.HasErrors())
            {
                commentServiceResponse.AddErrors(comment.GetErrors());
                return commentServiceResponse;
            }

            await _repository.Update(comment);
            _cachingDB.CreateEntry(CachingKey + comment.Id, _mapper.Map<CommentCachingModel>(comment));
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> DeleteComment(int postId, Guid userId, int commentId)
    {
        var commentServiceResponse = new CommentServiceResponse();

        var cachedComment = _cachingDB.RetrieveEntry<CommentCachingModel>(CachingKey + commentId);

        var comment = cachedComment is not null ?
            _mapper.Map<Comment>(cachedComment) :
            await _repository.FindById(commentId);
        if (comment is null) commentServiceResponse.AddError($"Comment with id '{commentId}' not found");

        if (comment is not null && postId != comment.PostId)
            commentServiceResponse.AddError($"You can't delete the comment. Post id does not match");

        if (comment is not null && userId != comment.UserId)
            commentServiceResponse.AddError($"You can't delete the comment. User id does not match");

        if (!commentServiceResponse.HasErrors())
        {
            await _repository.Delete(comment!);
            _cachingDB.DeleteEntry(CachingKey + comment!.Id);
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> GetCommentInteractions(int commentId)
    {
        var commentServiceResponse = new CommentServiceResponse();

        try
        {
            var cachedInteractions = 
                _cachingDB.RetrieveEntry<List<CommentInteractionCachingModel>>(CommentInterationsKey + commentId);


            commentServiceResponse.Interactions = cachedInteractions is not null ?
                _mapper.Map<List<CommentInteraction>>(cachedInteractions) : 
                await _repository.GetInteractions(commentId);

            if (cachedInteractions is null)
                _cachingDB.CreateEntry(
                    CommentInterationsKey + commentId,
                    _mapper.Map<List<CommentInteractionCachingModel>>(commentServiceResponse.Interactions)
                );
        }
        catch (CommentNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> AddCommentInteraction(CommentInteraction interaction)
    {
        var commentServiceResponse = new CommentServiceResponse();

        try
        {
            await _repository.AddInteraction(interaction);
            commentServiceResponse.Interaction = interaction;
            _cachingDB.DeleteEntry(CommentInterationsKey + commentServiceResponse.Interaction.Id);
        }
        catch (CommentNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }
        catch (PostNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }
        catch (UserNotFoundException e)
        {
            commentServiceResponse.AddError(e.Message);
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> UpdateCommentInteraction(int commentId, Guid userId, int interactionId, InteractionStatus status)
    {
        var commentServiceResponse = new CommentServiceResponse();

        var interaction = await _repository.GetInteractionById(interactionId);
        if (interaction is null) commentServiceResponse.AddError($"Comment interaction with id '{interactionId}' not found");

        if (interaction is not null && commentId != interaction.CommentId)
            commentServiceResponse.AddError($"You can't update the comment interaction. Comment id does not match");

        if (interaction is not null && userId != interaction.UserId)
            commentServiceResponse.AddError($"You can't update the comment interaction. User id does not match");

        if(!commentServiceResponse.HasErrors())
        {
            interaction!.UpdateStatus(status);

            if(interaction.HasErrors())
            {
                commentServiceResponse.AddErrors(interaction.GetErrors());
                return commentServiceResponse;
            }

            await _repository.UpdateInteraction(interaction);
            _cachingDB.DeleteEntry(CommentInterationsKey + interaction.Id);
        }

        return commentServiceResponse;
    }

    public async Task<CommentServiceResponse> DeleteCommentInteraction(int commentId, Guid userId, int interactionId)
    {
        var commentServiceResponse = new CommentServiceResponse();

        var interaction = await _repository.GetInteractionById(interactionId);
        if (interaction is null) commentServiceResponse.AddError($"Comment interaction with id '{interactionId}' not found");

        if (interaction is not null && commentId != interaction.CommentId)
            commentServiceResponse.AddError($"You can't update the comment interaction. Comment id does not match");

        if (interaction is not null && userId != interaction.UserId)
            commentServiceResponse.AddError($"You can't update the comment interaction. User id does not match");

        if (!commentServiceResponse.HasErrors())
        {
            await _repository.DeleteInteraction(interaction!);
            _cachingDB.DeleteEntry(CommentInterationsKey + interaction!.Id);
        }

        return commentServiceResponse;
    }
}