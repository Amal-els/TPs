using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TP4.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Membership> memberships { get; set; }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===================== Genres =====================
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Action" },
                new Genre { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Comedy" },
                new Genre { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Drama" }
            );

            // ===================== Memberships =====================
            modelBuilder.Entity<Membership>().HasData(
                new Membership { Id = 1, TypeName = "Basic", Discount = 5 },
                new Membership { Id = 2, TypeName = "Premium", Discount = 15 },
                new Membership { Id = 3, TypeName = "VIP", Discount = 25 }
            );

            // ===================== Movies =====================
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Stock = 5,
                    GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    ReleaseDate = new DateTime(2008, 7, 18),
                    Stock = 3,
                    GenreId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Hangover",
                    ReleaseDate = new DateTime(2009, 6, 5),
                    Stock = 2,
                    GenreId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Movie
                {
                    Id = 4,
                    Title = "Forrest Gump",
                    ReleaseDate = new DateTime(1994, 7, 6),
                    Stock = 4,
                    GenreId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                }
            );

            // ===================== Customers =====================
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FullName = "Alice Smith", IsSubscribed = true, MembershipId = 2 },
                new Customer { Id = 2, FullName = "Bob Johnson", IsSubscribed = false, MembershipId = 1 },
                new Customer { Id = 3, FullName = "Charlie Brown", IsSubscribed = true, MembershipId = 3 }
            );
        }
    }
}   
