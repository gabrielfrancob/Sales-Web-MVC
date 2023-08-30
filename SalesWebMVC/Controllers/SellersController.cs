using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly DepartmentService _departmentsService;
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService, DepartmentService departmentsService)
        {
            _sellerService = sellerService;
            _departmentsService = departmentsService;
        }

        //ACTIONS
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult FindById(int id)
        {
            var seller = _sellerService.FindById(id);
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //if (!ModelState.IsValid) return View();
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) return BadRequest();
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

        // VIEWS
        public IActionResult Details(int id)
        {
            if (id == null) return NotFound();

            var obj = _sellerService.FindById(id);
            if (obj == null) return NotFound();

            return View(obj);
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentsService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var obj = _sellerService.FindById(id.Value);
            if (obj == null) return NotFound();

            return View(obj);

        }

        public IActionResult Edit(int id)
        {
            if (id == null) return NotFound();

            var seller = _sellerService.FindById(id);
            if (seller == null) return NotFound();

            List<Department> departments = _departmentsService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }
    }
}
