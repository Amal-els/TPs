using Microsoft.EntityFrameworkCore;
using TP4.Models;
using TP4.Repositories;

namespace TP4.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Movie> GetActionMoviesInStock()
        {
            return _unitOfWork.Movies
                .GetMoviesWithGenre()
                .Where(m => m.Genre.Name == "Action" && m.Stock > 0)
                .ToList();
        }

        public List<Movie> GetMoviesOrdered()
        {
            return _unitOfWork.Movies
                .GetMoviesWithGenre()
                .OrderBy(m => m.ReleaseDate)
                .ThenBy(m => m.Title)
                .ToList();
        }

        public int GetTotalMoviesCount()
        {
            return _unitOfWork.Movies.GetAll().Count();
        }

        public List<MovieGenreDTO> GetMoviesWithGenres()
        {
            var query = from m in _unitOfWork.Movies.GetAll()
                        join g in _unitOfWork.Genres.GetAll()
                        on m.GenreId equals g.Id
                        select new MovieGenreDTO
                        {
                            Title = m.Title,
                            Genre = g.Name
                        };

            return query.ToList();
        }

        public List<Genre> GetTop3PopularGenres()
        {
            return _unitOfWork.Genres
                .GetAllWithIncludes(g => g.Movies)
                .OrderByDescending(g => g.Movies.Count)
                .Take(3)
                .ToList();
        }

        public void AddMovie(Movie movie)
        {
            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Complete();
        }

        public void UpdateMovie(Movie movie)
        {
            _unitOfWork.Movies.Update(movie);
            _unitOfWork.Complete();
        }

        public void DeleteMovie(int id)
        {
            var movie = _unitOfWork.Movies.GetById(id);
            if (movie != null)
            {
                _unitOfWork.Movies.Remove(movie);
                _unitOfWork.Complete();
            }
        }
    }
}