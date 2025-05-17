using API.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Med_Safe_Net_API.Entities
{
    public class UserRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }
        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        [JsonIgnore]
        public AppUser? User { get; set; }
        [ForeignKey(nameof(Role))]
        public AppRoleType AppRoleId { get; set; }
        [JsonIgnore]
        public AppRole? Role { get; set; }
    }
}
