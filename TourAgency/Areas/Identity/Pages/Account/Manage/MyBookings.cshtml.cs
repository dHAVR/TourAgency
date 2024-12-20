using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TourAgency.Data;
using TourAgency.Models;

namespace TourAgency.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class MyBookingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyBookingsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Booking> Bookings { get; set; }

        [BindProperty]
        public ReviewInputModel ReviewInput { get; set; }

        public class ReviewInputModel
        {
            public int BookingId { get; set; }
            public string Comment { get; set; }
        }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            Bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Tour) 
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCancelAsync(int bookingId)
        {
            var userId = _userManager.GetUserId(User);
            var booking = await _context.Bookings
                .Where(b => b.Id == bookingId && b.UserId == userId)
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking); 
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostLeaveReviewAsync()
        {
            var booking = await _context.Bookings.FindAsync(ReviewInput.BookingId);
            if (booking != null && booking.IsConfirmed)
            {
                var review = new Review
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    TourId = booking.TourId,
                    Comment = ReviewInput.Comment,
                    CreatedAt = DateTime.Now,
                    IsVerified = false 
                };
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
 
}
}
