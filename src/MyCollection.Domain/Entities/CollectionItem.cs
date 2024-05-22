using MyCollection.Domain.Common.Auditables;

namespace MyCollection.Domain.Entities;
public class CollectionItem : Entity
{
    public Guid OwnerId { get; set; }

    public Guid CollectionId { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTimeOffset CreatedDate { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public User Owner { get; set; }
}
