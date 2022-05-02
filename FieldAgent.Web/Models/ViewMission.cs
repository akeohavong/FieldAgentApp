using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Web.Models
{
    public class ViewMission
    {
        public int missionId { get; set; }
        [Required(ErrorMessage = "Agency ID is required")]
        public int agencyId { get; set; }

        //[Required(ErrorMessage ="Codename is required")]
        [StringLength(50, ErrorMessage ="Codename cannot exceed 50 characters")]
        public string codename { get; set; }
        public string notes { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "Projected End Date is required")]
        public DateTime projectedEndDate { get; set; }
        public DateTime actualEndDate { get; set; }
        [Precision (8,2)]
        public decimal operationalCost { get; set; }


    }
}
