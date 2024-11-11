using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourAgency.Data;
using TourAgency.Models;
using TourAgency.ViewModels;

namespace TourAgency.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _dbContext.Bookings.Include(b => b.Tour).ToListAsync();
            var reviews = await _dbContext.Reviews.Include(r => r.Tour).Include(r => r.User).ToListAsync();

            var viewModel = new AdminDashboardViewModel
            {
                Bookings = bookings,
                Reviews = reviews
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTour(Tour tour, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        tour.Image = memoryStream.ToArray();
                    }
                }

                _dbContext.Tours.Add(tour);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tour);
        }
    }
}
