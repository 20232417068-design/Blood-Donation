using BloodDonation.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add MVC
builder.Services.AddControllersWithViews();

// Add Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add DB (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// =================== APPLY MIGRATIONS + DEFAULT ADMIN =====================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate(); // Apply migrations automatically

    // Insert default admin only if no admin exists
    if (!db.Admins.Any())
    {
        db.Admins.Add(new BloodDonation.Models.Admin
        {
            Username = "admin",
            Password = "admin123"
        });

        db.SaveChanges();
    }
}
// ==========================================================================

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
