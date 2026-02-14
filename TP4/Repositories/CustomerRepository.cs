using Microsoft.EntityFrameworkCore;
using TP4.Models;

namespace TP4.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }
        
        public IEnumerable<Customer> GetCustomersWithMembership()
        {
            return GetAll().Include(c => c.Membership);
        }

        public IEnumerable<Customer> GetSubscribedCustomers()
        {
            return GetAll()
                .Include(c => c.Membership)
                .Where(c => c.IsSubscribed);
        }
    }
}
