namespace TourAgency.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfSpots { get; set; }
        public bool IsConfirmed { get; set; } = false; 

        public virtual Tour Tour { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
