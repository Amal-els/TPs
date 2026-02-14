using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int Id { get; set; } 
    public string Name { get; set; }
}
