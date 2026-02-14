using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP5.Models;
using TP5.Data;

namespace YourProjectName.Controllers
{
    [Authorize]
    public class PanierController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PanierController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PanierParUser()
        {
            var currentUserId = _userManager.GetUserId(User);
            var paniers = _context.Paniers
                .Where(p => p.UserID == currentUserId)
                .ToList();

            // Build a dictionary: ProduitId → Nom
            var produitIds = paniers.Select(p => p.ProduitId).ToList();

            var produits = _context.Produits
                .Where(pr => produitIds.Contains(pr.Id))
                .ToDictionary(pr => pr.Id, pr => pr.Nom);

            ViewBag.ProduitNames = produits;

            return View(paniers);
        }


        public IActionResult AddToBasket(Guid produitId)
        {
            var currentUserId = _userManager.GetUserId(User);

            var panier = new PanierParUser
            {
                Id = Guid.NewGuid(),
                UserID = currentUserId,
                ProduitId = produitId
            };

            _context.Paniers.Add(panier);
            _context.SaveChanges();

            return RedirectToAction("PanierParUser");
        }

        public IActionResult Index()
        {
            var produits = _context.Produits.ToList();
            return View(produits);
        }


        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var currentUserId = _userManager.GetUserId(User);

            var item = _context.Paniers
                .FirstOrDefault(p => p.Id == id && p.UserID == currentUserId);

            if (item == null)
            {
                return NotFound();
            }

            _context.Paniers.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("PanierParUser");
        }

    }
}