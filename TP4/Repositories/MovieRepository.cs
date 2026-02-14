using System;
using Microsoft.EntityFrameworkCore;
using TP4.Models;

namespace TP4.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) { }
        public IEnumerable<Movie> GetMoviesWithGenre()
        {
            return GetAll().Include(m => m.Genre);
        }

        public IEnumerable<Movie> GetMoviesInStock()
        {
            return GetAll().Where(m => m.Stock > 0);
        }

        public IEnumerable<Movie> GetMoviesByGenre(Guid genreId)
        {
            return GetAll()
                .Include(m => m.Genre)
                .Where(m => m.GenreId == genreId);
        }
    }
}
