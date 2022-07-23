namespace MediaApp.Api.Controllers.V1;

[ApiVersion("1.0")]
public class CommentsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;

    public CommentsController(IMapper mapper, ICommentService commentService)
    {
        _mapper = mapper;
        _commentService = commentService;
    }

    [HttpGet(ApiRoutes.CommentRoutes.PostId)]
    public async Task<IActionResult> GetAllPostComments(int postId)
    {
        var result = await _commentService.GetAllPostComments(postId);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<List<CommentResponse>>(result.Comments));
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> CreateComment([FromBody] CreateComment comment)
    {
        var newComment = Comment.builder(
            comment.PostId,
            comment.UserId,
            comment.Text
        );

        if(newComment.HasErrors()) return BadRequest(GetErrorResponse(newComment.GetErrors()));

        var result = await _commentService.CreateComment(newComment);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<CommentResponse>(result.Payload));
    }

    [HttpPut(ApiRoutes.CommentRoutes.UpdateComment)]
    [ValidateModel]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateComment comment)
    {
        var result = await _commentService.UpdateComment(
            comment.PostId, 
            comment.UserId, 
            commentId, 
            comment.Text
        );

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Comment updated successfully");
    }

    [HttpDelete(ApiRoutes.CommentRoutes.DeleteComment)]
    public async Task<IActionResult> DeleteComment(int commentId, int postId, string userId)
    {
        CheckIfPathVariableIsValidGuid(userId);

        var result = await _commentService.DeleteComment(
            postId,
            Guid.Parse(userId),
            commentId
        );

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Comment deleted successfully");
    }

    [HttpGet(ApiRoutes.CommentRoutes.CommentInteractions)]
    public async Task<IActionResult> GetCommentInteractions(int commentId)
    {
        var result = await _commentService.GetCommentInteractions(commentId);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<List<CommentInteractionResponse>>(result.Interactions));
    }

    [HttpPost(ApiRoutes.CommentRoutes.CommentInteractions)]
    [ValidateModel]
    public async Task<IActionResult> AddCommentInteraction(int commentId, [FromBody] AddCommentInteraction interaction)
    {
        var newInteraction = CommentInteraction.builder(
            commentId,
            interaction.UserId,
            interaction.Status
        );

        if (newInteraction.HasErrors()) return BadRequest(GetErrorResponse(newInteraction.GetErrors()));

        var result = await _commentService.AddCommentInteraction(newInteraction);
        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<CommentInteractionResponse>(result.Interaction));
    }

    [HttpPut(ApiRoutes.CommentRoutes.UpdateCommentInteraction)]
    [ValidateModel]
    public async Task<IActionResult> UpdateCommentInteraction(
        int commentId, 
        int interactionId,
        [FromBody] UpdateCommentInteraction interaction
    )
    {
        var result = await _commentService.UpdateCommentInteraction(
            commentId,
            interaction.UserId,
            interactionId,
            interaction.Status
        );

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Comment interaction updated successfully");
    }

    [HttpDelete(ApiRoutes.CommentRoutes.DeleteCommentInteraction)]
    [ValidateModel]
    public async Task<IActionResult> DeleteCommentInteraction(
        int commentId,
        int interactionId,
        string userId
    )
    {
        CheckIfPathVariableIsValidGuid(userId);

        var result = await _commentService.DeleteCommentInteraction(
            commentId,
            Guid.Parse(userId),
            interactionId
        );

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Comment interaction updated successfully");
    }
}