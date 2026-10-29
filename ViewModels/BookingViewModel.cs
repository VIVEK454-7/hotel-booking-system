using System.ComponentModel.DataAnnotations;
namespace StayEase.ViewModels;
public class BookingViewModel
{
    public int RoomId { get; set; }
    public string RoomName { get; set; } = "";
    public decimal PricePerNight { get; set; }
    [Required, StringLength(100)] public string GuestName { get; set; } = "";
    [Required, EmailAddress] public string GuestEmail { get; set; } = "";
    [Required, DataType(DataType.Date), Display(Name = "Check-in")] public DateTime CheckIn { get; set; } = DateTime.Today.AddDays(1);
    [Required, DataType(DataType.Date), Display(Name = "Check-out")] public DateTime CheckOut { get; set; } = DateTime.Today.AddDays(2);
    [Range(1, 10)] public int Guests { get; set; } = 2;
}
