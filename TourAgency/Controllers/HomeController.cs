using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TourAgency.Data;
using TourAgency.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Localization;

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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
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
            var tour = _context.Tours
                .Include(t => t.Reviews.Where(r => r.IsVerified)) 
                .ThenInclude(r => r.User) 
                .FirstOrDefault(t => t.Id == id);

            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(int tourId, string firstName, string lastName, string phoneNumber, int numberOfSpots)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var tour = _context.Tours.Find(tourId);
            if (tour == null || tour.AvailableSlots < numberOfSpots)
            {
                return NotFound(); 
            }

            var booking = new Booking
            {
                TourId = tourId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                BookingDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                NumberOfSpots = numberOfSpots,
                IsConfirmed = false 
            };

            tour.AvailableSlots -= numberOfSpots; 
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("BookingConfirmation");
        }




        public IActionResult BookingForm(int tourId)
        {
            var tour = _context.Tours.Find(tourId);
            if (tour == null)
            {
                return NotFound(); 
            }

            return View(tour); 
        }


        public IActionResult BookingConfirmation()
        {
            return View();
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
