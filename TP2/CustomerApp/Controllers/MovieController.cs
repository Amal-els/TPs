using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _db;

    public MovieController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Index avec tri et pagination
    public IActionResult Index(string sortOrder, int page = 1)
    {
        int pageSize = 5; // movies per page
        ViewData["NameSortParm"] = sortOrder == "name_desc" ? "" : "name_desc";

        var movies = _db.Movies.Include(m => m.Genre).AsQueryable();

        // Dynamic sort
        movies = sortOrder == "name_desc" ? movies.OrderByDescending(m => m.Name)
                                        : movies.OrderBy(m => m.Name);

        // Pagination
        var pagedMovies = movies.Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

        // Calculate total pages
        ViewBag.TotalPages = (int)Math.Ceiling((double)movies.Count() / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.SortOrder = sortOrder;

        return View(pagedMovies);
    }


    // GET: Create
    public IActionResult Create()
    {
        ViewBag.Genres = _db.Genres.ToList(); // pour dropdown
        return View();
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Movie movie)
    {
        if (ModelState.IsValid)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Genres = _db.Genres.ToList();
        return View(movie);
    }

    // GET: Edit
    public IActionResult Edit(int id)
    {
        var movie = _db.Movies.Find(id);
        if (movie == null) return NotFound();
        ViewBag.Genres = _db.Genres.ToList();
        return View(movie);
    }

    // POST: Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Movie movie)
    {
        if (ModelState.IsValid)
        {
            _db.Movies.Update(movie);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Genres = _db.Genres.ToList();
        return View(movie);
    }

    // GET: Delete
    public IActionResult Delete(int id)
    {
        var movie = _db.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie);
    }

    // POST: DeleteConfirmed
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var movie = _db.Movies.Find(id);
        if (movie != null)
        {
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
{
    // Include Genre to display it in the view
    var movie = _db.Movies
                   .Include(m => m.Genre)
                   .FirstOrDefault(m => m.Id == id);

    if (movie == null)
        return NotFound();

    return View(movie);
}
}
