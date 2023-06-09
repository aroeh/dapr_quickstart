using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DojoService.Models
{
    public class Dojo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}