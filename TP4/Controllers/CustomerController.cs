using Microsoft.AspNetCore.Mvc;
using TP4.Services;

namespace TP4.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) => _customerService = customerService;

         public IActionResult Index()
        {
            // Get all customers with their membership info
            var customers = _customerService.GetAllCustomersWithMembership();
            return View(customers);
        }
        public IActionResult SubscribedWithDiscount()
        {
            var customers = _customerService.GetSubscribedCustomersWithDiscount();
            return View(customers);
        }
    }
}
