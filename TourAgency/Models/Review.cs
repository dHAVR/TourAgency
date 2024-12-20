﻿namespace TourAgency.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }       
        public int TourId { get; set; }           
        public string Comment { get; set; }       
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
