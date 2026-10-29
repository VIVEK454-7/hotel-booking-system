using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayEase.Data;

namespace StayEase.Controllers;
public class HomeController(StayEaseContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Rooms.AsNoTracking().ToListAsync());
    public IActionResult Privacy() => View();
    public IActionResult Error() => View();
}
