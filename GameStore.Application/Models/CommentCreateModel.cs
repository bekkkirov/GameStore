namespace GameStore.Application.Models;

public class CommentCreateModel
{
    public string Body { get; set; }

    public int GameId { get; set; }

    public int? ParentCommentId { get; set; }
}