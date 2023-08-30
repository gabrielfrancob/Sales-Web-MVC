using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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
        public async Task<IActionResult> FindById(int id)
        {
            var seller = await _sellerService.FindByIdAsync(id);
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //if (!ModelState.IsValid) return View();
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id não coincidentes." }); ;
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        // VIEWS
        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = _sellerService.FindByIdAsync(id);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado" });

            return View(obj);
        }
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentsService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado" });

            return View(obj);

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var seller = await _sellerService.FindByIdAsync(id);
            if (seller == null) return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado" });

            List<Department> departments = await _departmentsService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);

        }
    }
}
