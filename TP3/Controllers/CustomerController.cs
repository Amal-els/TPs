using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TP3.Models.ViewModels;
using TP3.Models;
using Microsoft.EntityFrameworkCore;
namespace TP3.Controllers;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _db;
    public CustomerController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
        {
            // Récupérer tous les customers avec leurs informations de membership
            var customers = await _db.Customers
                .Include(c => c.Membershiptypes)  // Inclure les données de membership
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(customers);
        }

        public IActionResult Create()
    {
        // Dropdown shows SignUpFee, value is Id
        ViewBag.MembershipTypes = new SelectList(
            _db.Membershiptypes.OrderBy(m => m.SignUpFee),
            "Id",
            "SignUpFee"
        );
        return View();
    }

    // POST: Customer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Customers customer)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.MembershipTypes = new SelectList(
                _db.Membershiptypes.OrderBy(m => m.SignUpFee),
                "Id",
                "SignUpFee",
                customer.membershiptype_Id // preselect
            );
            return View(customer);
        }

        customer.Id = Guid.NewGuid(); // Ensure new GUID
        _db.Customers.Add(customer);
        _db.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

   [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var customer = _db.Customers.Find(id);
        if (customer == null) return NotFound();

        ViewBag.MembershipTypes = new SelectList(_db.Membershiptypes.OrderBy(m => m.SignUpFee), "Id", "SignUpFee", customer.membershiptype_Id);
        return View(customer);
    }

    // POST: Customer/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Customers customer)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.MembershipTypes = new SelectList(_db.Membershiptypes.OrderBy(m => m.SignUpFee), "Id", "SignUpFee", customer.membershiptype_Id);
            return View(customer);
        }

        _db.Customers.Update(customer);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Customer/Details/{id}
    public IActionResult Details(Guid id)
    {
        var customer = _db.Customers
                          .Include(c => c.Membershiptypes)
                          .Include(c => c.Movies)
                          .FirstOrDefault(c => c.Id == id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    // GET: Customer/Delete/{id}
    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        var customer = _db.Customers
                          .Include(c => c.Membershiptypes)
                          .FirstOrDefault(c => c.Id == id);

        if (customer == null) return NotFound();

        return View(customer);
    }

    // POST: Customer/Delete/{id}
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
        var customer = _db.Customers.Find(id);
        if (customer == null) return NotFound();

        _db.Customers.Remove(customer);
        _db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}