using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace TP3.Controllers;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _db;
    public GenreController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        var genres = _db.genres.ToList();
        return View(genres);
    }

[HttpGet]
public IActionResult Create()
{   
    return View();
}

    [HttpPost]
    [ValidateAntiForgeryToken] // ✅ Protection anti-CSRF
    public IActionResult Create(Genres genre)
    {
        if (ModelState.IsValid)
        {
            _db.genres.Add(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // Extract all errors from ModelState
        ViewBag.Errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        ViewBag.Genres = new SelectList(_db.genres.ToList(), "Id", "GenreName");

        return View(genre);
    }
[HttpGet]
        public IActionResult Delete(Guid id)
        {
            var genre = _db.genres
                           .Include(g => g.Movies) // Include movies to check associations
                           .FirstOrDefault(g => g.Id == id);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // POST: Genre/Delete/{id} -> Delete genre
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var genre = _db.genres
                           .Include(g => g.Movies)
                           .FirstOrDefault(g => g.Id == id);

            if (genre == null)
                return NotFound();

            // Prevent deletion if genre has movies
            if (genre.Movies.Any())
            {
                TempData["ErrorMessage"] = "Cannot delete this genre because it has associated movies.";
                return RedirectToAction(nameof(Index));
            }

            _db.genres.Remove(genre);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Genre deleted successfully!";
            return RedirectToAction(nameof(Index));
        }



[HttpGet]
public IActionResult Edit(Guid id)
{
    var genre = _db.genres.Find(id);
    if (genre == null)
        return NotFound();

    return View(genre);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Genres genre)
{
    if (!ModelState.IsValid)
        return View(genre);

    _db.genres.Update(genre);
    _db.SaveChanges();
    return RedirectToAction(nameof(Index));
}

[HttpGet]
public IActionResult Details(Guid id)
{
    // Include related movies using EF Core's Include
    var genre = _db.genres
                   .Include(g => g.Movies)
                   .FirstOrDefault(g => g.Id == id);

    if (genre == null)
        return NotFound();

    return View(genre);
}





}