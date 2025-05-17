using System.ComponentModel.DataAnnotations;

namespace Med_Safe_Net_API.Entities
{
    public enum AppRoleType : int
    {
        Patient = 1,
        Caregiver = 2,
        Administrator = 3,
    }
    public class AppRole
    {
        [Key]
        public AppRoleType AppRoleId { get; set; }
        [Required]
        public string? AppRoleName { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
