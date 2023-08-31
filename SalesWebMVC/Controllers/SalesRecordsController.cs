using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            this.salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");
            var result = await salesRecordsService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public IActionResult GroupingSearch() { return View(); }
    }
}
