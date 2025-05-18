using API.Entities;
using Med_Safe_Net_API.Entities;
using System.Collections.ObjectModel;

namespace Med_Safe_Net_API.DTOs
{
    public class PatientInfoDto
    {
        public int Id { get; set; }
        public Guid? UserCode { get; set; } = Guid.NewGuid();
        public required string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<HeartRate>? HeartRates { get; set; }
        public ICollection<SuddenMovement>? SuddenMovements { get; set; }
        public ICollection<HighHeartRate>? HighHeartRates { get; set; }
    }
}
