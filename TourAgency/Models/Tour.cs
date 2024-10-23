namespace TourAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }

        public string Name { get; set; }  

        public string Description { get; set; }  

        public decimal Price { get; set; }
        public byte[]? Image { get; set; }

        public DateTime StartDate { get; set; }  

        public DateTime EndDate { get; set; }  

        public int AvailableSlots { get; set; }  
    }
}
