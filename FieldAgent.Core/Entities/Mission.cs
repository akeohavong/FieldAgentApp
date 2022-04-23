

using System.ComponentModel.DataAnnotations;

namespace FieldAgent.Core.Entities
{
    public class Mission
    {
        [Key]
        public int MissionID { get; set; }
        [Required]
        public int AgencyID { get; set; }
        [Required, MaxLength(50)]
        public string CodeName { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ProjectedEndDate { get; set; }
        [Required]
        public DateTime ActualEndDate { get; set; }
        [Required]
        public decimal OperationalCost { get; set; }
        public Agency Agency { get; set; }
        public List<Agent> Agents { get; set; }
    }
}
