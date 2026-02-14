
using TP3.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureDeleted();  // ⚠️ Deletes the database
    db.Database.EnsureCreated();

    Console.WriteLine("📦 Connected to database:");
    Console.WriteLine(db.Database.GetDbConnection().ConnectionString);

    if (!db.genres.Any() && !db.Movies.Any())
    {
        var action = new Genres { GenreName = "Action" };
        var comedy = new Genres { GenreName = "Comedy" };
        var drama = new Genres { GenreName = "Drama" };
        db.genres.AddRange(action, comedy, drama);
        db.SaveChanges();
        var films = new List<Movies>
        {
            new Movies { Name = "Fast & Furious", genre_Id = action.Id },
            new Movies { Name = "Die Hard", genre_Id = action.Id },
            new Movies { Name = "The Hangover", genre_Id = comedy.Id },
            new Movies { Name = "Superbad", genre_Id = comedy.Id },
            new Movies { Name = "The Godfather", genre_Id = drama.Id },
            new Movies { Name = "Shawshank Redemption", genre_Id = drama.Id },
            new Movies { Name = "Inception", genre_Id = action.Id },
            new Movies { Name = "Step Brothers", genre_Id = comedy.Id }
        };

        db.Movies.AddRange(films);
        db.SaveChanges();
        Console.WriteLine("✅ Database seeded successfully!");

    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "ByRelease",
    pattern: "movies/released/{year}/{month}",
    defaults: new { controller = "Movie", action = "ByRelease" }
);

app.Run();
