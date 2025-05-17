using Med_Safe_Net_API.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Username { get; set; }
    public required byte[] PasswordHash {get;set;}
    public required byte[] PasswordSalt {get;set;}
    public DateTime DateOfBirth { get; set; }
    public required string Email {get;set;}
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<HeartRate>? HeartRates { get; set; }
    public ICollection<SuddenMovement>? SuddenMovements { get; set; }
    public ICollection<HighHeartRate>? HighHeartRates { get; set; }
    public ICollection<UserRole>? UserRoles { get; set; } = new Collection<UserRole>();

}
