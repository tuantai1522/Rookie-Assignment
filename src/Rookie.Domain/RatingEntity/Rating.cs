using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Domain.RatingEntity
{
    public class Rating : BaseEntity
    {
        public RatingId Id { get; set; } = new RatingId(Guid.NewGuid());

        [JsonIgnore]
        public ProductId? ProductId { get; set; }
        public Product? Product { get; set; }

        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? ApplicationUser { get; set; }

        [Column(TypeName = "int")]
        public RatingValue Value { get; set; }
        public string? Comment { get; set; }
    }
}