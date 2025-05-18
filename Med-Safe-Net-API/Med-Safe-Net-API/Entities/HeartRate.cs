using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class HeartRate
{
    [Key]
    public int HeartRateId { get; set; }
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public double Measurement { get; set; }
    [JsonIgnore]
    public AppUser User { get; set; }
}
