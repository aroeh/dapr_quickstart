using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class MemberRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("dojoId")]
        [Required]
        public string DojoId { get; set; } = string.Empty;

        [BsonElement("name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
