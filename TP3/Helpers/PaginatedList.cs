using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YourProjectNamespace.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }  // Page actuelle
        public int TotalPages { get; private set; } // Nombre total de pages

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        // Indique s'il y a une page précédente
        public bool HasPreviousPage => PageIndex > 1;

        // Indique s'il y a une page suivante
        public bool HasNextPage => PageIndex < TotalPages;

        // Méthode pour créer un PaginatedList à partir d'un IQueryable
        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count(); // nombre total d'éléments
            var items = source.Skip((pageIndex - 1) * pageSize) // ignorer les éléments précédents
                              .Take(pageSize)                  // prendre seulement pageSize éléments
                              .ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
