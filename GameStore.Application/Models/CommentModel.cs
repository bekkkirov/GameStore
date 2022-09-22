namespace GameStore.Application.Models;

public class CommentModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Body { get; set; }

    public List<CommentModel> Replies { get; set; }
}