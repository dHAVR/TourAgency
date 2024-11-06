using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TourAgency.Data;
using TourAgency.Models;
using System.Linq;

namespace TourAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var tours = _context.Tours.Take(3).ToList();
            return View(tours);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var tour = _context.Tours.Find(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        public IActionResult Tours(string searchQuery, string countryFilter, decimal? minPrice, decimal? maxPrice, bool onlyAvailable = false)
        {
            var tours = _context.Tours.AsQueryable();

            if (onlyAvailable)
            {
                tours = tours.Where(t => t.AvailableSlots > 0);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                tours = tours.Where(t => t.Name.Contains(searchQuery) || t.Description.Contains(searchQuery));
            }

            if (minPrice.HasValue)
            {
                tours = tours.Where(t => t.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                tours = tours.Where(t => t.Price <= maxPrice);
            }

            return View(tours.ToList());
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
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
