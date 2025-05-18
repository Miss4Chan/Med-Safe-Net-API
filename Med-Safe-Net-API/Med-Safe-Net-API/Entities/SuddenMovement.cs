using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class SuddenMovement
{
    [Key]
    public int MovementId { get; set; }
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public bool? Confirm { get; set; }
    public DateTime? TimeOfConfirmation { get; set; }
    [JsonIgnore]
    public AppUser User { get; set; }
}

