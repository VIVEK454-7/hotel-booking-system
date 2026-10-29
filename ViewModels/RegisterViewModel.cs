using System.ComponentModel.DataAnnotations;
namespace StayEase.ViewModels;
public class RegisterViewModel
{
    [Required, StringLength(80, MinimumLength = 2)] public string FullName { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, MinLength(8), DataType(DataType.Password)] public string Password { get; set; } = "";
    [Required, Compare(nameof(Password)), DataType(DataType.Password), Display(Name = "Confirm password")] public string ConfirmPassword { get; set; } = "";
}
