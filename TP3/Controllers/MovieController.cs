using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP3.Models;
using TP3.Models.ViewModels;
using YourProjectNamespace.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TP3.Controllers;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IWebHostEnvironment _webHostEnvironment;  


    public MovieController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment )
    {
        _db = db;
         _webHostEnvironment = webHostEnvironment;

    }


public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
{

    var movies = from m in _db.Movies
                 select m;


    // Tri dynamique
    movies = sortOrder switch
    {
        "title_desc" => movies.OrderByDescending(m => m.Name),
        _ => movies.OrderBy(m => m.Name),
    };

    // Pagination simple
    int pageSize = 3;
    return View(PaginatedList<Movies>.Create(movies.AsNoTracking(), pageNumber ?? 1, pageSize));
     
}
[HttpGet]
public IActionResult Details(Guid id)
{  
    var movie = _db.Movies
    .Include(m=>m.Genres)
    .FirstOrDefault(m => m.Id == id);

    if (movie == null)
    {
        return NotFound();
    }

    return View(movie);
    
}



// POST: Movie/Delete/{id} -> Delete the movie
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(Guid id)
{
    var movie = _db.Movies.Find(id);

    if (movie == null)
        return NotFound();

    _db.Movies.Remove(movie);
    _db.SaveChanges();
    return RedirectToAction(nameof(Index));
}

    

[HttpGet]
public async Task<IActionResult> Edit(Guid id)
{
    var movie = await _db.Movies.FindAsync(id);

    if (movie == null)
        return NotFound();

    // Populate genres for dropdown and preselect the movie's current genre
    ViewBag.Genres = new SelectList(
        _db.genres.OrderBy(g => g.GenreName),
        "Id",
        "GenreName",
        movie.genre_Id
    );

    return View(movie);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Guid id, Movies film, IFormFile? photo)
{
    if (id != film.Id) 
        return NotFound();

    // ✅ Remove navigation properties from ModelState validation
    ModelState.Remove("ImageFile");
    ModelState.Remove("Genres");  // ✅ Important! Navigation property
    ModelState.Remove("Customers");  // ✅ Important! Navigation property
    
    // ✅ Log validation errors for debugging
    if (!ModelState.IsValid)
    {
        var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { 
                Field = x.Key, 
                Errors = string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage)) 
            })
            .ToList();
        
        TempData["Error"] = $"Validation failed: {string.Join(" | ", errors.Select(e => $"{e.Field}: {e.Errors}"))}";
        
        ViewBag.Genres = new SelectList(
            _db.genres.OrderBy(g => g.GenreName),
            "Id",
            "GenreName",
            film.genre_Id
        );
        return View(film);
    }

    try
    {
        // Fetch existing movie with tracking
        var movieToUpdate = await _db.Movies.FindAsync(id);
        if (movieToUpdate == null) 
            return NotFound();

        // Update fields
        movieToUpdate.Name = film.Name;
        movieToUpdate.Month = film.Month;
        movieToUpdate.Year = film.Year;
        movieToUpdate.genre_Id = film.genre_Id;
        
        // ✅ Only update DateAjoutMovie if it has a value
        if (film.DateAjoutMovie.HasValue)
        {
            movieToUpdate.DateAjoutMovie = film.DateAjoutMovie;
        }

        // Handle optional file upload
        if (photo != null && photo.Length > 0)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("photo", "Invalid image format");
                ViewBag.Genres = new SelectList(
                    _db.genres.OrderBy(g => g.GenreName),
                    "Id",
                    "GenreName",
                    film.genre_Id
                );
                return View(film);
            }

            // Validate size
            if (photo.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("photo", "File size must not exceed 5 MB");
                ViewBag.Genres = new SelectList(
                    _db.genres.OrderBy(g => g.GenreName),
                    "Id",
                    "GenreName",
                    film.genre_Id
                );
                return View(film);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            
            if (!Directory.Exists(imagesPath)) 
                Directory.CreateDirectory(imagesPath);

            var filePath = Path.Combine(imagesPath, uniqueFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // ✅ Delete old image if exists
            if (!string.IsNullOrEmpty(movieToUpdate.ImageFile))
            {
                var oldImagePath = Path.Combine(imagesPath, movieToUpdate.ImageFile);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            movieToUpdate.ImageFile = uniqueFileName;
        }

        // Save changes (no need to explicitly mark as modified since we used FindAsync)
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Film modifié avec succès!";
        return RedirectToAction(nameof(Index));
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!MovieExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    catch (Exception ex)
    {
        TempData["Error"] = $"Erreur lors de la modification: {ex.Message}";
        ViewBag.Genres = new SelectList(
            _db.genres.OrderBy(g => g.GenreName),
            "Id",
            "GenreName",
            film.genre_Id
        );
        return View(film);
    }
}

// ✅ Helper method
private bool MovieExists(Guid id)
{
    return _db.Movies.Any(e => e.Id == id);
}



  
   // ============================================
// GET: Movie/Create
// ============================================
[HttpGet]
public IActionResult Create()
{
    ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
    return View();
}

// ============================================
// POST: Movie/Create
// ============================================
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(MovieVM model, IFormFile photo)
{
     if (model == null)
    {
        return BadRequest("Model is null");
    }
    // Validation de la photo
    if (photo == null || photo.Length == 0)
    {
        ModelState.AddModelError("photo", "Veuillez sélectionner une image");
        ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
        return View(model);
    }

    // Validation de l'extension
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();

    if (!allowedExtensions.Contains(extension))
    {
        ModelState.AddModelError("photo", "Seuls les fichiers image (jpg, jpeg, png, gif) sont autorisés");
        ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
        return View(model);
    }

    if (model.movie == null)
    {
        ModelState.AddModelError("", "Movie data is missing");
        ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
        return View(model);
    }

    // Validation de la taille (max 5MB)
    if (photo.Length > 5 * 1024 * 1024)
    {
        ModelState.AddModelError("photo", "La taille du fichier ne doit pas dépasser 5 MB");
        ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
        return View(model);
    }

    try
    {
        // Générer un nom de fichier unique
        var uniqueFileName = Guid.NewGuid().ToString() + extension;

        // Créer le chemin du dossier images
        var imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        // Créer le répertoire s'il n'existe pas
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        var filePath = Path.Combine(imagesPath, uniqueFileName);

        // Sauvegarder le fichier
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        // ✅ Créer le movie avec Guid.NewGuid() (pas new Guid())
        var movie = new Movies
        {
            Id = Guid.NewGuid(),  // ✅ Génère un GUID unique
            Name = model.movie.Name,
            Month = model.movie.Month,
            Year = model.movie.Year,
            genre_Id = model.movie.genre_Id,
            DateAjoutMovie = DateTime.Now,
            ImageFile = uniqueFileName
        };

        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();

        TempData["SuccessMessage"] = "Film créé avec succès!";
        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        ModelState.AddModelError("", $"Erreur lors de la création: {ex.Message}");
        ViewBag.Genres = new SelectList(_db.genres.OrderBy(g => g.GenreName), "Id", "GenreName");
        return View(model);
    }
}


}

