using MyCollection.Domain.Common.Auditables;
using NpgsqlTypes;

namespace MyCollection.Domain.Entities;
public class Comment : Entity
{
    public Guid SenderId { get; set; }

    public User Sender { get; set; }

    public Guid ItemId { get; set; }

    public Guid? ParentId { get; set; }

    public string Message { get; set; } = default!;

    public NpgsqlTsVector SearchVector { get; set; }

    public DateTimeOffset SendAt { get; set; }

    public ICollection<Comment> Comments { get; set;} = new List<Comment>();

}
