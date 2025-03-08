using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dogs.Models;
using Dogs.Services;

namespace EjemplosVespMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DogService _dogService;

        public HomeController(ILogger<HomeController> logger, DogService dogService)
        {
            _logger = logger;
            _dogService = dogService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int limit = 10; // Número de perros por página
            var (breeds, totalRecords) = await _dogService.GetBreedsAsync(page, limit);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / limit);
            return View(breeds);
        }

        public async Task<IActionResult> Details(string id)
        {
            var dog = await _dogService.GetDogDetailsAsync(id);
            return View(dog);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
