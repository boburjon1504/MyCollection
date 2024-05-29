using MyCollection.Domain.Common.Auditables;

namespace MyCollection.Domain.Entities;
public class Tag : Entity
{
    public string Name { get; set; } = default!;

    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();
}
