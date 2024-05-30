using Microsoft.AspNetCore.Http;
using MyCollection.Domain.Common.Auditables;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCollection.Domain.Entities;
public class CollectionItem : Entity
{
    public Guid OwnerId { get; set; }

    public Guid CollectionId { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string ImgPath { get; set; } = default!;

    public int LikesCount { get; set; } 

    public int CommentsCount { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public NpgsqlTsVector SearchVector { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();

    public User Owner { get; set; }

    public Collection Collection { get; set; }

    [NotMapped]
    public IFormFile ImgForm { get; set; }
}
