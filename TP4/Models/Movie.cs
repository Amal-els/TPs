namespace TP4.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int Stock { get; set; }

        public Guid GenreId { get; set; }

        public Genre? Genre { get; set; }

        public Movie()
        {
        }
    }
}
