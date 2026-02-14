using TP4.Models;

namespace TP4.Services
{
    public interface ICustomerService
    {
        List<Customer> GetSubscribedCustomersWithDiscount();
        List<Customer> GetAllCustomersWithMembership();

    }
}
