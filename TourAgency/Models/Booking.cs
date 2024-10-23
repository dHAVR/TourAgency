namespace TourAgency.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int TourId { get; set; }        
        public string UserId { get; set; }      
        public DateTime BookingDate { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
