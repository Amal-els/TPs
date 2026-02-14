using System.Collections.Generic;
using TP3.Models;
namespace TP3.Models.ViewModels;

    public class CustomerMoviesViewModel
    {
        public Customers customers { get; set; }
        public List<Movies> movies { get; set; }
    }
