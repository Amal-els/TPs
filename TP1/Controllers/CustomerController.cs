using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP1.Models;
using TP1.Models.ViewModels;
using YourProjectNamespace.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TP1.Controllers;

public class CustomerController : Controller
{
    public IActionResult CustomerDetails(int id)
{
    // Client statique
    var customer = new Customer { Id = id, Name = "Amal Bahri" };

    // Liste de films statique
    var movies = new List<Movie>
    {
        new Movie { Id = 1, Name = "Movie 1" },
        new Movie { Id = 2, Name = "Movie 2" },
        new Movie { Id = 3, Name = "Movie 3" }
    };

    var vm = new CustomerMoviesViewModel
    {
        Customer = customer,
        Movies = movies
    };

    return View(vm);
}
     public IActionResult CustomerMovieDetails (int id)
  {
    List<Movie> movies = new List<Movie>()
    { 
      new Movie { Id=1, Name="Movie 1" },
      new Movie { Id=2, Name="Movie 2" },
      new Movie { Id=3, Name="Movie 3" },
     };
  
    var movie = movies.FirstOrDefault(m => m.Id == id);

    if (movie == null)
    {
        return NotFound();
    }

    return View(movie);
  }

}