using TP4.Models;

namespace TP4.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IEnumerable<Customer> GetCustomersWithMembership();
        IEnumerable<Customer> GetSubscribedCustomers();
    }
}
