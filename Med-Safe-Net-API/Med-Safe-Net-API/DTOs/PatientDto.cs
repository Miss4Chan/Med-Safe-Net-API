using API.Entities;
using Med_Safe_Net_API.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Med_Safe_Net_API.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public Guid? UserCode { get; set; } = Guid.NewGuid();
        public required string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; } = new Collection<UserRole>();
        public ICollection<UserLink>? UserLinks { get; set; } = new Collection<UserLink>();
    }
}
