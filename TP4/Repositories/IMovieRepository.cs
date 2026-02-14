using TP4.Models;

namespace TP4.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        IEnumerable<Movie> GetMoviesWithGenre();
        IEnumerable<Movie> GetMoviesInStock();
        IEnumerable<Movie> GetMoviesByGenre(Guid genreId);
    }
}
