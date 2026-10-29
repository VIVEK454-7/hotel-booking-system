using System.ComponentModel.DataAnnotations;

namespace StayEase.Models;

public class Room
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [Required, StringLength(60)] public string Category { get; set; } = "";
    [Required, StringLength(500)] public string Description { get; set; } = "";
    [Range(1, 1000000)] public decimal PricePerNight { get; set; }
    [Range(1, 10)] public int Capacity { get; set; }
    [Required, StringLength(500)] public string ImageUrl { get; set; } = "";
    [StringLength(40)] public string Badge { get; set; } = "";
}
