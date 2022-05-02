using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class ViewAgent
    {
        public int agentId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength (50, ErrorMessage ="First name cannot exceed 50 characters")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime dateOfBirth { get; set; }
        [Precision(3,2)]
        public decimal height { get; set; }
    }
}
