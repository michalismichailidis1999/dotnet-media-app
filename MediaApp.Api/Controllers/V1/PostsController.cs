

namespace MediaApp.Api.Controllers.V1;

[ApiVersion("1.0")]
public class PostsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IPostService _postService;

    public PostsController(IMapper mapper, IPostService postService)
    {
        _mapper = mapper;
        _postService = postService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPosts();
        return Ok(_mapper.Map<List<PostResponse>>(posts));
    }

    [Authorize]
    [HttpGet(ApiRoutes.PostRoutes.PostId)]
    public async Task<IActionResult> GetPostById(int id)
    {
        var result = await _postService.GetPostById(id);

        if(result.HasErrors()) return BadRequest(result.GetErrors());

        return Ok(_mapper.Map<PostResponse>(result.Payload));
    }

    [Authorize]
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> CreatePost([FromBody] CreatePost post)
    {
        var newPost = Post.builder(
            post.UserId,
            post.Text
        );

        if (newPost.HasErrors()) return BadRequest(GetErrorResponse(newPost.GetErrors()));

        var result = await _postService.CreatePost(newPost);
        if(result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return CreatedAtAction(
            nameof(GetPostById), 
            new { id = result.Payload!.Id },
            _mapper.Map<PostResponse>(result.Payload)
        );
    }

    [Authorize]
    [HttpPut(ApiRoutes.PostRoutes.PostId)]
    [ValidateModel]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePost post)
    {
        var result = await _postService.UpdatePost(id, post.UserId, post.Text);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Post updated successfully");
    }

    [Authorize]
    [HttpDelete(ApiRoutes.PostRoutes.DeletePost)]
    public async Task<IActionResult> DeletePost(int id, string userId)
    {
        CheckIfPathVariableIsValidGuid(userId);

        var userIdParsed = Guid.Parse(userId);

        var result = await _postService.DeletePost(id, userIdParsed);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Post deleted successfully");
    }

    [Authorize]
    [HttpPost(ApiRoutes.PostRoutes.PostInteractions)]
    [ValidateModel]
    public async Task<IActionResult> AddInteractionToPost(int postId, [FromBody] AddPostInteraction interaction)
    {
        var newInteraction = PostInteraction.builder(
            postId,
            interaction.UserId,
            interaction.Status
        );

        if (newInteraction.HasErrors()) return BadRequest(GetErrorResponse(newInteraction.GetErrors()));

        var result = await _postService.AddInteractionToPost(newInteraction);

        if(result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<PostInteractionResponse>(result.Interaction));
    }

    [Authorize]
    [HttpGet(ApiRoutes.PostRoutes.PostInteractions)]
    public async Task<IActionResult> GetPostInteractions(int postId)
    {
        var result = await _postService.GetPostInteractions(postId);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok(_mapper.Map<List<PostInteractionResponse>>(result.Interactions));
    }

    [Authorize]
    [HttpPut(ApiRoutes.PostRoutes.UpdateInteraction)]
    public async Task<IActionResult> UpdatePostInteraction(
        int postId, 
        int interactionId,
        [FromBody] UpdatePostInteraction interaction
    )
    {
        var result = await _postService.UpdatePostInteraction(
            postId,
            interaction.UserId,
            interactionId,
            interaction.Status
        );

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Post interaction updated successfully");
    }

    [Authorize]
    [HttpDelete(ApiRoutes.PostRoutes.DeleteInteraction)]
    public async Task<IActionResult> DeletePostInteraction(int postId, string userId, int interactionId)
    {
        CheckIfPathVariableIsValidGuid(userId);

        var userIdParsed = Guid.Parse(userId);

        var result = await _postService.DeletePostInteraction(postId, userIdParsed, interactionId);

        if (result.HasErrors()) return BadRequest(GetErrorResponse(result.GetErrors()));

        return Ok("Post interaction deleted successfully");
    }
}