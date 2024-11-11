using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, 10000, ErrorMessage = "Price must be between $1 and $10,000.")]
        public decimal Price { get; set; }

        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Available slots are required.")]
        [Range(1, 100, ErrorMessage = "Available slots must be between 1 and 100.")]
        public int AvailableSlots { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
