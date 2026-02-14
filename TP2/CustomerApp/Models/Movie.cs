using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Movie
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    // Relation: each Movie has a Genre
    public Guid GenreId { get; set; }
    public Genre? Genre { get; set; }
}
