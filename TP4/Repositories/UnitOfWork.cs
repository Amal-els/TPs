using TP4.Models;

namespace TP4.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IMovieRepository Movies { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IGenericRepository<Genre> Genres { get; private set; }
        public IGenericRepository<Membership> Memberships { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Movies = new MovieRepository(_context);
            Customers = new CustomerRepository(_context);
            Genres = new GenericRepository<Genre>(_context);
            Memberships = new GenericRepository<Membership>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}