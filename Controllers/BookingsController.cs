using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayEase.Data;
using StayEase.Models;
using StayEase.ViewModels;

namespace StayEase.Controllers;
public class BookingsController(StayEaseContext db) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create(int roomId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Bookings", new { roomId }) });
        var room = await db.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id == roomId);
        if (room is null) return NotFound();
        var user = await db.Users.FindAsync(userId.Value);
        return View(new BookingViewModel { RoomId = room.Id, RoomName = room.Name, PricePerNight = room.PricePerNight, GuestName = user?.FullName ?? "", GuestEmail = user?.Email ?? "", CheckIn = DateTime.Today.AddDays(1), CheckOut = DateTime.Today.AddDays(2), Guests = 2 });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookingViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");
        var room = await db.Rooms.FirstOrDefaultAsync(r => r.Id == model.RoomId);
        if (room is null) return NotFound();
        if (model.CheckIn.Date < DateTime.Today) ModelState.AddModelError(nameof(model.CheckIn), "Check-in cannot be in the past.");
        if (model.CheckOut.Date <= model.CheckIn.Date) ModelState.AddModelError(nameof(model.CheckOut), "Check-out must be at least one day after check-in.");
        if (model.Guests > room.Capacity) ModelState.AddModelError(nameof(model.Guests), $"This room accommodates up to {room.Capacity} guests.");
        if (!ModelState.IsValid)
        {
            model.RoomName = room.Name; model.PricePerNight = room.PricePerNight;
            return View(model);
        }
        var nights = (model.CheckOut.Date - model.CheckIn.Date).Days;
        var booking = new Booking { StayUserId = userId.Value, RoomId = room.Id, GuestName = model.GuestName.Trim(), GuestEmail = model.GuestEmail.Trim().ToLowerInvariant(), CheckIn = model.CheckIn.Date, CheckOut = model.CheckOut.Date, Guests = model.Guests, Nights = nights, TotalPrice = room.PricePerNight * nights, Status = "Confirmed" };
        db.Bookings.Add(booking);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Confirmation), new { id = booking.Id });
    }

    public async Task<IActionResult> Confirmation(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");
        var booking = await db.Bookings.Include(b => b.Room).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id && b.StayUserId == userId.Value);
        return booking is null ? NotFound() : View(booking);
    }

    public async Task<IActionResult> Mine()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");
        var bookings = await db.Bookings.Include(b => b.Room).Where(b => b.StayUserId == userId.Value).OrderByDescending(b => b.CreatedAt).AsNoTracking().ToListAsync();
        return View(bookings);
    }
}
