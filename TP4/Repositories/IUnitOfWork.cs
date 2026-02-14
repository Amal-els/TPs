using TP4.Models;
namespace TP4.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        ICustomerRepository Customers { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<Membership> Memberships { get; }
        
        int Complete();
        Task<int> CompleteAsync();
    }
}