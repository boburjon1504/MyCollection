using MyCollection.Domain.Common.Auditables;

namespace MyCollection.Domain.Entities;
public class ItemTag : Entity
{
    public Guid TagId { get; set; }

    public Guid ItemId { get; set; }

    public Tag Tag { get; set; }

    public CollectionItem Item { get; set; }
}
