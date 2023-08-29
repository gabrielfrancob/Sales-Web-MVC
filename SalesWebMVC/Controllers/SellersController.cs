using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesWebMVCContext _context;

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService, SalesWebMVCContext context)
        {
            _context = context;
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            seller.Department = _context.Department.First();
            //if (!ModelState.IsValid) return View();
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
