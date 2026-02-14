
using TP1.Models;
using System.Collections.Generic;

namespace TP1.Models.ViewModels
{

    public class CustomerMoviesViewModel
    {
        public Customer Customer { get; set; }
        public List<Movie> Movies { get; set; }
    }

}