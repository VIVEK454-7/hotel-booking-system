using System.ComponentModel.DataAnnotations;

namespace StayEase.Models;

public class StayUser
{
    public int Id { get; set; }
    [Required, StringLength(80)] public string FullName { get; set; } = "";
    [Required, EmailAddress, StringLength(160)] public string Email { get; set; } = "";
    [Required] public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
