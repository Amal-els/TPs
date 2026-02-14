using Microsoft.EntityFrameworkCore;
using TP4.Models;
using TP4.Repositories;

namespace TP4.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Customer> GetAllCustomersWithMembership()
        {
            return _unitOfWork.Customers
                .GetCustomersWithMembership()
                .OrderBy(c => c.FullName)
                .ToList();
        }

        public List<Customer> GetSubscribedCustomersWithDiscount()
        {
            return _unitOfWork.Customers
                .GetSubscribedCustomers()
                .Where(c => c.Membership.Discount > 10)
                .ToList();
        }

        public void AddCustomer(Customer customer)
        {
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();
        }

        public void UpdateCustomer(Customer customer)
        {
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);
            if (customer != null)
            {
                _unitOfWork.Customers.Remove(customer);
                _unitOfWork.Complete();
            }
        }
    }
}