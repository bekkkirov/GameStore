using GameStore.Domain.Common;

namespace GameStore.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Image ProfileImage { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
}