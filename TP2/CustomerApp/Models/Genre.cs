using System.ComponentModel.DataAnnotations;

public class Genre
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    // One-to-many relation
    public ICollection<Movie>? Movies { get; set; }
}
