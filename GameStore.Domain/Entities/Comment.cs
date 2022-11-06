using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class Comment : BaseEntity
{
    public string Body { get; set; }

    public bool IsRoot { get; set; }

    public bool IsMarkedForDeletion { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public int? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }

    public List<Comment> Replies { get; set; } = new List<Comment>();
}