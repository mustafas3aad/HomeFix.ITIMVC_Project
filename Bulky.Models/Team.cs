using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Years Of Experience")]
        [Range(1, 1000)]

        public int YearsOfExperience { get; set; }
        [Display(Name = "Available 24/7")]
        public bool IsAvailable24Hours { get; set; } = true;
   
        [Required]
        [Display(Name = "Non-Refundable Deposit (Online Payment)")]
        [Range(500, 1000)]
        public double DepositAmount { get; set; }

        [Display(Name = "Deposit Description")]
        public string? DepositNote { get; set; } = "This is a non-refundable deposit that must be paid online upon booking";

        [Required]
        [Display(Name = "Price Per Hour (EGP) - Cash Payment")]
        [Range(200, 500, ErrorMessage = "Hourly rate must be between 10 and 500 EGP")]
        [DataType(DataType.Currency)]
        public double HourlyRate { get; set; }

        [Display(Name = "Number of Workers")]
        [Range(1, 100)]
        public int WorkersCount { get; set; }
       

        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        [ValidateNever]
        public Service Service { get; set; }
    
        [ValidateNever]
        public List<TeamImages> TeamImages { get; set;}
    }
}
