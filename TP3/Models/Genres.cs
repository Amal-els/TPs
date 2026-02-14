using TP3.Models;
namespace TP3.Models;
public class Genres
{
    public Guid Id { get; set; }
    public  string GenreName { get; set; }
    public ICollection<Movies> Movies { get; set; } = new List<Movies>();

}