# StayEase — Hotel Booking System

StayEase is a hotel-room booking web application built as an academic Web.NET project. It features a responsive hotel-inspired interface, account registration and login, room browsing, booking validation, booking confirmation, and a personal booking history.

## Features
- Responsive landing page with room cards and photography
- Sign up and login; passwords are stored as PBKDF2 hashes (not plaintext)
- Session-based user state and logout
- Room booking with check-in/check-out dates and guest-count validation
- Automatic total calculation based on nights and room price
- Booking confirmation/reference number and “My bookings” history
- MySQL persistence through Entity Framework Core and Pomelo
- Database and sample rooms created automatically on first startup

## Tech stack
- ASP.NET Core MVC / C#
- Razor Views, HTML, CSS, JavaScript
- MySQL
- Entity Framework Core 9 + Pomelo.EntityFrameworkCore.MySql 9

## Requirements
- .NET 10 SDK
- MySQL Server 8.x (or a compatible MySQL server)

## Run locally
1. Install and start MySQL Server.
2. Open `appsettings.json` and replace `YOUR_MYSQL_PASSWORD` with your local MySQL password. Do not commit real credentials.
3. Open a terminal in the folder containing `StayEase.csproj` and run:

   ```powershell
   dotnet restore
   dotnet build
   dotnet run
   ```

4. Open the localhost URL printed in the terminal. The application creates `StayEaseDB` tables and seeds sample rooms on first startup, provided the configured MySQL account has permission to create the database.

If your MySQL user cannot create databases, create `StayEaseDB` manually in Workbench first, then run the app again.

## Database tables
- `Users` — registered users and password hashes
- `Rooms` — seeded demo room catalogue
- `Bookings` — booking details, dates, guest count, total, and status

This starter uses `EnsureCreated()` for straightforward academic setup rather than EF migrations. If the schema changes later, update the database carefully; `EnsureCreated()` does not apply migrations to an existing schema.

## Important notes
- The project is an educational demo, not a production booking platform. It does not process payments or prevent overlapping reservations for the same room.
- Room photos are loaded from Unsplash and require internet access. Images are illustrative; room availability and prices are sample data.
- Use a unique local password and keep secrets out of source control.
