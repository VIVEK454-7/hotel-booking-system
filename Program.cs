using Microsoft.EntityFrameworkCore;
using StayEase.Data;
using StayEase.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(3);
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("YOUR_MYSQL_PASSWORD"))
    throw new InvalidOperationException("Set your MySQL password in appsettings.json before running StayEase.");
builder.Services.AddDbContext<StayEaseContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// Create the schema and add sample rooms for a first-time local setup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StayEaseContext>();
    db.Database.EnsureCreated();
    if (!db.Rooms.Any())
    {
        db.Rooms.AddRange(
            new Room { Name = "Coastal King Suite", Category = "Luxury Suite", Description = "A calm, spacious suite with a king bed, warm lighting and a private balcony.", PricePerNight = 6800m, Capacity = 2, ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=1200&q=85", Badge = "Guest favourite" },
            new Room { Name = "Garden Deluxe Room", Category = "Deluxe Room", Description = "A thoughtfully designed room with soft textures, garden views and all the essentials.", PricePerNight = 4200m, Capacity = 2, ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=1200&q=85", Badge = "Great value" },
            new Room { Name = "The Heritage Suite", Category = "Premium Suite", Description = "Classic character meets modern comfort, with a lounge area for a slower stay.", PricePerNight = 8900m, Capacity = 3, ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=1200&q=85", Badge = "Premium" },
            new Room { Name = "City View Twin", Category = "Twin Room", Description = "A bright twin room with generous space, clean lines and a beautiful city outlook.", PricePerNight = 3600m, Capacity = 2, ImageUrl = "https://images.unsplash.com/photo-1595576508898-0ad5c879a061?auto=format&fit=crop&w=1200&q=85", Badge = "Popular" }
        );
        db.SaveChanges();
    }
}
app.Run();
