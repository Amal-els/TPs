using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _db;

    public GenreController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Index
    public IActionResult Index()
    {
        var genres = _db.Genres.ToList();
        return View(genres);  // Vue fortement typée List<Genre>
    }

    // GET: Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _db.Genres.Add(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: Edit
    public IActionResult Edit(Guid id)
    {
        var genre = _db.Genres.Find(id);
        if (genre == null) return NotFound();
        return View(genre);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Genre genre)
    {
        if (ModelState.IsValid)
        {
            _db.Genres.Update(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(genre);
    }

    // GET: Delete
    public IActionResult Delete(Guid id)
    {
        var genre = _db.Genres.Find(id);
        if (genre == null) return NotFound();
        return View(genre);
    }

    // POST: DeleteConfirmed
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
        var genre = _db.Genres.Find(id);
        if (genre != null)
        {
            _db.Genres.Remove(genre);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Details(Guid id)
    {
        var genre = _db.Genres.Find(id);
        if (genre == null)
            return NotFound(); // return 404 if not found

        return View(genre); // strongly-typed view: Genre
    }

}
