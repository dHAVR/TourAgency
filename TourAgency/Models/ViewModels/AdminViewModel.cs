namespace TourAgency.Models.ViewModels
{
    public class AdminViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Tour> Tours { get; set; }
        public Tour NewTour { get; set; } 
    }
}
