using TourAgency.Models;

namespace TourAgency.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Booking> Bookings { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
