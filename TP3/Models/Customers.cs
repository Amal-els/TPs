using System.ComponentModel.DataAnnotations;

namespace TP3.Models;

public class Customers
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est obligatoire.")]

    public string  Name { get; set; }   
    public Guid? membershiptype_Id { get; set; }

    public ICollection<Movies>? Movies { get; set; } 
    public  Membershiptypes? Membershiptypes { get; set; }
}