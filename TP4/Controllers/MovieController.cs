using Microsoft.AspNetCore.Mvc;
using TP4.Services;

namespace TP4.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var moviesWithGenres = _movieService.GetMoviesWithGenres();
            return View(moviesWithGenres); 
        }

        public IActionResult ActionInStock()
        {
            var movies = _movieService.GetActionMoviesInStock();
            return View(movies); 
        }

        public IActionResult Ordered()
        {
            var model = _movieService.GetMoviesOrdered();
            return View(model);
        }

        public IActionResult Count()
        {
            var total = _movieService.GetTotalMoviesCount();
            return View(total);
        }

        public IActionResult TopGenres()
        {
            var genres = _movieService.GetTop3PopularGenres();
            return View(genres);
        }


    }
}
