using TP4.Models;

namespace TP4.Services
{
    public interface IMovieService
    {  
        List<Movie> GetActionMoviesInStock();
        List<Movie> GetMoviesOrdered();
        int GetTotalMoviesCount();
        List<MovieGenreDTO> GetMoviesWithGenres();
        List<Genre> GetTop3PopularGenres();

    }
}
