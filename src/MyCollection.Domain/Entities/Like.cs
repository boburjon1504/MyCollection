using MyCollection.Domain.Common.Auditables;

namespace MyCollection.Domain.Entities;
public class Like : Entity
{
    public Guid ItemId { get; set; }

    public Guid UserId { get; set; }

}
