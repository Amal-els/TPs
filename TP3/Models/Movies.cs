using Microsoft.AspNetCore.Mvc;

namespace TP3.Models;
public class Movies
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string? ImageFile { get; set; }
     // Foreign Key vers Genre
    public Guid genre_Id { get; set; }

    // Navigation property : chaque film appartient à un genre
    public Genres Genres { get; set; }
    public ICollection<Customers> Customers { get; set; }
    public DateTime? DateAjoutMovie { get; set; }
    
}
