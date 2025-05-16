using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class HighHeartRate
{
    [Key]
    public int HighHeartRateId { get; set; }
    public double Measurement { get; set; }
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public bool? Confirm { get; set; }
    public DateTime? TimeOfConfirmation { get; set; }
    public required AppUser User { get; set; }
}
