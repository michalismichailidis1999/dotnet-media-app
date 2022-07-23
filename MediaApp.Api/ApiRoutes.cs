namespace MediaApp.Api;

public class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

    public class UserRoutes
    {
        public const string Register = "register";
        public const string Login = "login";
        public const string UserId = "{id}";
        public const string UpdateFirstName = "first_name";
        public const string UpdateLastName = "last_name";
        public const string UpdateEmail = "email";
        public const string UpdateUsername = "username";
        public const string UpdatePassword = "password";
    }

    public class PostRoutes
    {
        public const string PostId = "{id}";
        public const string DeletePost = "{id}/{userId}";
        public const string PostInteractions = "{postId}/interactions";
        public const string UpdateInteraction = "{postId}/interactions/{interactionId}";
        public const string DeleteInteraction = "{postId}/interactions/{interactionId}/{userId}";
    }

    public class CommentRoutes
    {
        public const string PostId = "{postId}";
        public const string UpdateComment = "{commentId}";
        public const string DeleteComment = "{commentId}/{postId}/{userId}";
        public const string CommentInteractions = "{commentId}/interactions";
        public const string UpdateCommentInteraction = "{commentId}/interactions/{interactionId}";
        public const string DeleteCommentInteraction = "{commentId}/interactions/{interactionId}/{userId}";
    }
}