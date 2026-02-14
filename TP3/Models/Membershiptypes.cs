namespace TP3.Models;

public class Membershiptypes
{
    public Guid Id { get; set; }
    public string  SignUpFee { get; set; }  
    public int DurationInMonths { get; set; }
    public double DiscountRate { get; set; } 
    public ICollection<Customers> Customers { get; set; } 
}