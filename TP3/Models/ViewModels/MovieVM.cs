using System.ComponentModel.DataAnnotations;
using TP3.Models;
namespace TP3.Models.ViewModels;

public class MovieVM
{
public Movies movie { get; set; }


[Display(Name = "Movie Poster")]

public IFormFile? photo { get; set; }
}