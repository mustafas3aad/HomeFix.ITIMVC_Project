using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter your comment")]
        [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please select a rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please select a service")]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

       
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
    }
}