namespace TP3.Models;

using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Movies>? Movies { get; set; }
    public DbSet<Genres> genres { get; set; }
    public DbSet<Customers> Customers { get; set; }
    public DbSet<Membershiptypes> Membershiptypes {  get; internal set; }
    public DbSet<AuditLog> AuditLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure la relation un-à-plusieurs explicitement (optionnel)
        modelBuilder.Entity<Movies>()
            .HasOne(m => m.Genres)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.genre_Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Customers>()
            .HasOne(c => c.Membershiptypes)             
            .WithMany(m => m.Customers)             
            .HasForeignKey(c => c.membershiptype_Id)     
            .OnDelete(DeleteBehavior.SetNull);      
            

        modelBuilder.Entity<Customers>()
            .HasMany(c => c.Movies)             
            .WithMany(m => m.Customers);             
        modelBuilder.Entity<Customers>()
            .HasMany(c => c.Movies)
            .WithMany(m => m.Customers)
            .UsingEntity("CustomerMovie");

            modelBuilder.Entity<Membershiptypes> (b =>
                {
                    b.Navigation("Customers");
                });
            modelBuilder.Entity<Membershiptypes>().HasData(
                new Membershiptypes
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    SignUpFee = "0",
                    DurationInMonths = 1,
                    DiscountRate = 0
                },
                new Membershiptypes
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    SignUpFee = "30",
                    DurationInMonths = 3,
                    DiscountRate = 10
                },
                new Membershiptypes
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    SignUpFee = "90",
                    DurationInMonths = 6,
                    DiscountRate = 15
                },
                new Membershiptypes
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    SignUpFee = "300",
                    DurationInMonths = 12,
                    DiscountRate = 20
                }
            );



    modelBuilder.Entity<Genres>().HasData(
        new Genres { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), GenreName = "Action" },
        new Genres { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), GenreName = "Comedy" }
    );


             var moviesJson = System.IO.File.ReadAllText("Data/Movies.json");
            var movies = JsonSerializer.Deserialize<List<Movies>>(moviesJson);

            if (movies != null)
            {
                foreach (var m in movies)
                {
                    modelBuilder.Entity<Movies>().HasData(m);
                }
            }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.AddInterceptors(new AuditInterceptor());
    base.OnConfiguring(optionsBuilder);
}
 
    


}