using MyCollection.Domain.Common.Auditables;
using MyCollection.Domain.Enums;

namespace MyCollection.Domain.Entities;
public class Collection : Entity
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public CollectionType Type { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
    
    public string ImgPath { get; set; } = default!;

    public ICollection<CollectionItem> Items { get; set; } = new List<CollectionItem>();

    public User Owner { get; set; }
}
