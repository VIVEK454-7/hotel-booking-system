using System.ComponentModel.DataAnnotations;

namespace StayEase.Models;

public class Booking
{
    public int Id { get; set; }
    public int StayUserId { get; set; }
    public StayUser? StayUser { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    [Required, StringLength(100)] public string GuestName { get; set; } = "";
    [Required, EmailAddress, StringLength(160)] public string GuestEmail { get; set; } = "";
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Guests { get; set; }
    public int Nights { get; set; }
    public decimal TotalPrice { get; set; }
    [StringLength(20)] public string Status { get; set; } = "Confirmed";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
