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

public class MovieController : Controller
{
  public IActionResult Index()
  { 
    Movie movie = new Movie {
    Id = 1, Name = "Movie 1"};
    List<Movie> movies = new List<Movie>()
    { movie, new Movie { Id=2, Name="Movie 2" },
    new Movie { Id=3, Name="Movie 3" },
    new Movie { Id=4, Name="Movie 4" }, };
    return View(movies);
  }

  public IActionResult Edit(int id)
  {
    return Content("Test Id" + id);
  }
  public IActionResult Details (int id)
  {
    List<Movie> movies = new List<Movie>()
    { 
      new Movie { Id=1, Name="Movie 1" },
      new Movie { Id=2, Name="Movie 2" },
      new Movie { Id=3, Name="Movie 3" },
      new Movie { Id=4, Name="Movie 4" }, };
  
    var movie = movies.FirstOrDefault(m => m.Id == id);

    if (movie == null)
    {
        return NotFound();
    }

    return View(movie);
  }

  [Route("Movie/released/{year:int}/{month:int}")]
  public IActionResult ByRelease(int year, int month)
  {
      return Content($"Movies released in {month:D2}/{year}");
  }

}