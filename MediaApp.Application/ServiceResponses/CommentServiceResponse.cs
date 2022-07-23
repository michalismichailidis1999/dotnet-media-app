namespace MediaApp.Application.ServiceResponses;

public class CommentServiceResponse : BaseContentServiceResponse<Comment, CommentInteraction>
{
    public List<Comment> Comments { get; set; }
}